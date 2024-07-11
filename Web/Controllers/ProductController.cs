using Entities.EntityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Services.DataService.Interfaces;
using Services.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Web.Constants;
using Web.Helpers;
using Web.Helpers.Interfaces;
namespace Web.Controllers
{
    public class ProductController : Controller
    {
        #region "Declared objects & Constructor"
        private readonly ProjectDbContext _db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICookieHelperService _cookieHelperService;
        private readonly ICredentialManagerService _credentialManager;
        private readonly IDisplayMessageHelper _displayMessageHelper;
        private readonly ICryptoHelperService _cryptoHelperService;
        private readonly IDateTimeHelperService _dateTimeHelperService;
        private readonly IFileUploadHelperService _fileUploadHelperService;
        private readonly IProductCodeGeneratorService _productCodeGeneratorService;

        public ProductController(ProjectDbContext _db, IHttpContextAccessor httpContextAccessor, ICookieHelperService _cookieHelperService, ICredentialManagerService _credentialManager, IDisplayMessageHelper _displayMessageHelper, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService, IFileUploadHelperService _fileUploadHelperService, IProductCodeGeneratorService _productCodeGeneratorService)
        {
            this._db = _db;
            this.httpContextAccessor = httpContextAccessor;
            this._cookieHelperService = _cookieHelperService;
            this._credentialManager = _credentialManager;
            this._displayMessageHelper = _displayMessageHelper;
            this._cryptoHelperService = _cryptoHelperService;
            this._dateTimeHelperService = _dateTimeHelperService;
            this._fileUploadHelperService = _fileUploadHelperService;
            this._productCodeGeneratorService = _productCodeGeneratorService;
        }
        #endregion

        #region "Product Listing Related Methods"
        public async Task<IActionResult> Index()
        {
            _displayMessageHelper.ErrorMessageSetOrGet(this, false);
            _displayMessageHelper.SuccessMessageSetOrGet(this, false);
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> AjaxProducts(JqueryDataTableViewModel param)
        {
            try
            {
                int totalLength = 0;
                var displayLength = param.length;
                string search = HttpContext.Request.Query["search[value]"];

                var allResults = _db.Products.AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    allResults = allResults.Where(w => (!string.IsNullOrEmpty(w.ProductCode) && w.ProductCode.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.ProductName) && w.ProductName.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Size) && w.Size.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.Variant) && w.Variant.ToLower().Contains(search.ToLower())) || (!string.IsNullOrEmpty(w.BarcodeNumber) && w.BarcodeNumber.ToLower().Contains(search.ToLower())));
                }

                totalLength = allResults.Select(s => s.ProductId).Count();
                if (displayLength == -1)
                {
                    displayLength = totalLength;
                }
                var allUsers = await _db.AppUsers.AsQueryable().Select(s => new
                {
                    s.UserId,
                    s.UserName
                }).ToDictionaryAsync(keySelector: x => x.UserId, elementSelector: y => y.UserName);

                var displayedValues = (await allResults
                    .OrderByDescending(o => o.ProductId)
                    .Skip(param.start).Take(displayLength)
                    .ToArrayAsync())
                    .Select(s => new
                    {
                        productId = s.ProductId,
                        productStatus = s.IsActive != null ? s.IsActive : true,
                        modelNumber = !string.IsNullOrEmpty(s.ModelNumber) ? s.ModelNumber : "",
                        productCode = !string.IsNullOrEmpty(s.ProductCode) ? s.ProductCode : "",
                        productName = !string.IsNullOrEmpty(s.ProductName) ? s.ProductName : "",
                        brand = s.Brand,
                        size = s.Size,
                        variant = s.Variant,
                        unit = s.Unit,
                        barcodeNumber = !string.IsNullOrEmpty(s.BarcodeNumber) ? s.BarcodeNumber : "",
                        barcodeImagePath = !string.IsNullOrEmpty(s.BarcodeImage) ? s.BarcodeImage : "",
                        qrcodeText = !string.IsNullOrEmpty(s.QrcodeText) ? s.QrcodeText : "",
                        qrcodeImagePath = !string.IsNullOrEmpty(s.QrcodeImage) ? s.QrcodeImage : "",
                        releaseDate = s.ReleaseDate != null ? s.ReleaseDate.Value.ToString("dd/MM/yyyy") : "",
                        description = !string.IsNullOrEmpty(s.Description) ? s.Description : "",
                        productImageUrl = !string.IsNullOrEmpty(s.ProductImage) ? s.ProductImage : "",
                        createdBy = allUsers.ContainsKey(s.CreatedBy ?? 0) ? allUsers[s.CreatedBy ?? 0] : "",
                        creationDate = s.CreatedDate != null ? s.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                    }).ToList();

                return Json(new
                {
                    sEcho = param.draw,
                    iTotalRecords = displayedValues.Select(s => s.productId).Count(),
                    iTotalDisplayRecords = totalLength,
                    data = displayedValues
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    sEcho = 0,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    data = ""
                });
            }
        }
        #endregion

        #region "Product Add,Edit&Update Related Methods"
        public async Task<IActionResult> AddNew()
        {
            await Task.Delay(0);
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vmProductData = new VmProductData();
            try
            {
                if (id > 0)
                {
                    var productInfo = await _db.Products.AsQueryable()
                        .Where(w => w.ProductId == id)
                        .FirstOrDefaultAsync();
                    if (productInfo != null)
                    {
                        vmProductData.ProductId = productInfo.ProductId;
                        vmProductData.ProductCode = productInfo.ProductCode;
                        vmProductData.ProductName = productInfo.ProductName;
                        vmProductData.Brand = productInfo.Brand;
                        vmProductData.Size = productInfo.Size;
                        vmProductData.Variant = productInfo.Variant;
                        vmProductData.Unit = productInfo.Unit;
                        vmProductData.BarcodeNumber = productInfo.BarcodeNumber;
                        vmProductData.QRCodeText = productInfo.QrcodeText;
                        vmProductData.ModelNumber = productInfo.ModelNumber;
                        vmProductData.Description = productInfo.Description;
                        vmProductData.ProductStatus = productInfo.IsActive;
                        vmProductData.ReleaseDate = productInfo.ReleaseDate != null ? productInfo.ReleaseDate.Value.ToString("dd/MM/yyyy") : "";
                    }
                }
            }
            catch (Exception ex)
            {
                vmProductData = new VmProductData();
            }
            await Task.Delay(0);
            return View(vmProductData);
        }

        [HttpPost]
        public async Task<IActionResult> ProductAddEdit(VmProductData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentDateTime = _dateTimeHelperService.Now();
                    var userId = _cookieHelperService.GetUserIdFromCookie();
                    var existingData = await _db.Products.AsQueryable()
                        .Where(f => f.ProductId == model.ProductId)
                        .FirstOrDefaultAsync();
                    string productImgPath = _fileUploadHelperService.UploadFile(model.ProductImage, "product-images");
                    var releaseDate = _dateTimeHelperService.ConvertBDDateStringToDateTimeObject(model.ReleaseDate);

                    if (existingData != null)
                    {
                        existingData.ProductCode = model.ProductCode;
                        existingData.ProductName = model.ProductName;
                        existingData.Brand = model.Brand;
                        existingData.Size = model.Size;
                        existingData.Variant = model.Variant;
                        existingData.Unit = model.Unit;

                        if (existingData.BarcodeNumber != model.BarcodeNumber)
                        {
                            //remove previously generated Barcode
                            _productCodeGeneratorService.RemoveBarcode(existingData.BarcodeImage);

                            var newBarCodeImgPath = _productCodeGeneratorService.GenerateBarcode(model.ProductCode, model.BarcodeNumber);
                            if (!string.IsNullOrEmpty(newBarCodeImgPath))
                            {
                                existingData.BarcodeNumber = model.BarcodeNumber;
                                existingData.BarcodeImage = newBarCodeImgPath;
                            }
                        }
                        if (!string.IsNullOrEmpty(model.QRCodeText) && existingData.QrcodeText != model.QRCodeText)
                        {
                            //remove previously generated Qrcode
                            _productCodeGeneratorService.RemoveQRCode(existingData.QrcodeImage);

                            var newQRCodeImgPath = _productCodeGeneratorService.GenerateQRCode(model.ProductCode, model.QRCodeText);
                            if (!string.IsNullOrEmpty(newQRCodeImgPath))
                            {
                                existingData.QrcodeText = model.QRCodeText;
                                existingData.QrcodeImage = newQRCodeImgPath;
                            }
                        }

                        existingData.ModelNumber = model.ModelNumber;
                        existingData.Description = model.Description;
                        existingData.IsActive = model.ProductStatus;
                        if (!string.IsNullOrEmpty(releaseDate))
                            existingData.ReleaseDate = Convert.ToDateTime(releaseDate);
                        if (!string.IsNullOrEmpty(productImgPath))
                            existingData.ProductImage = productImgPath;
                        existingData.UpdatedBy = userId;
                        existingData.UpdatedDate = currentDateTime;
                        _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.PRODUCT_UPDATED);
                    }
                    else
                    {
                        var barCodeImgPath = _productCodeGeneratorService.GenerateBarcode(model.ProductCode, model.BarcodeNumber);
                        var qrCodeImgPath = _productCodeGeneratorService.GenerateQRCode(model.ProductCode, model.QRCodeText);
                        var newData = new Product
                        {
                            ProductCode = model.ProductCode,
                            ProductName = model.ProductName,
                            Brand = model.Brand,
                            Size = model.Size,
                            Variant = model.Variant,
                            Unit = model.Unit,
                            BarcodeNumber = !string.IsNullOrEmpty(barCodeImgPath) ? model.BarcodeNumber : "",
                            BarcodeImage = barCodeImgPath,
                            QrcodeText = !string.IsNullOrEmpty(qrCodeImgPath) ? model.QRCodeText : "",
                            QrcodeImage = qrCodeImgPath,
                            ModelNumber = model.ModelNumber,
                            Description = model.Description,
                            IsActive = model.ProductStatus,
                            ReleaseDate = !string.IsNullOrEmpty(releaseDate) ? Convert.ToDateTime(releaseDate) : null,
                            ProductImage = !string.IsNullOrEmpty(productImgPath) ? productImgPath : null,
                            CreatedBy = userId,
                            CreatedDate = currentDateTime,
                            UpdatedBy = userId,
                            UpdatedDate = currentDateTime,
                        };
                        _db.Products.Add(newData);
                        _displayMessageHelper.SuccessMessageSetOrGet(this, true, ConstantUserMessages.PRODUCT_CREATED);
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _displayMessageHelper.ErrorMessageSetOrGet(this, true, ex.Message);
                }
            }
            else
            {
                _displayMessageHelper.ErrorMessageSetOrGet(this, true, ConstantUserMessages.MODEL_STATE_INVALID);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region "Product Delete Related Methods"
        public async Task<IActionResult> DeleteProduct(int id)
        {
            string sucMsg = "";
            string errMsg = "";
            if (id > 0)
            {
                try
                {
                    var existingData = await _db.Products.AsQueryable().Where(f => f.ProductId == id).FirstOrDefaultAsync();
                    if (existingData != null)
                    {
                        //remove generated Barcode Image
                        _productCodeGeneratorService.RemoveBarcode(existingData.BarcodeImage);
                        //remove generated Qrcode Image
                        _productCodeGeneratorService.RemoveQRCode(existingData.QrcodeImage);
                        //remove uploaded product image
                        _fileUploadHelperService.RemoveFile(existingData.ProductImage, "product-images");
                        _db.Products.Remove(existingData);
                        await _db.SaveChangesAsync();
                        sucMsg = ConstantUserMessages.PRODUCT_DELETED;
                    }
                    else
                        errMsg = "Error occurred, no data found with this id!";
                }
                catch (Exception ex) { errMsg = ex.Message; }
            }
            else
                errMsg = "Error occurred, required id is missing!";
            return Json(new { sucMessage = sucMsg, errMessage = errMsg });
        }
        #endregion
    }
}
