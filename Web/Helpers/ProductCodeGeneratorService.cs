using System;
using QRCoder;
using SkiaSharp;
using System.IO;
using BarcodeStandard;
using System.Drawing.Imaging;
using Web.Helpers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Linq;
using Castle.Core.Internal;

namespace Web.Helpers
{
    public class ProductCodeGeneratorService : IProductCodeGeneratorService
    {
        #region "Constructor"
        private readonly IWebHostEnvironment _env;

        public ProductCodeGeneratorService(IWebHostEnvironment env)
        {
            _env = env;
        }
        #endregion

        public string GenerateBarcode(string productCode, string barCodeText)
        {
            string returnPath = "";
            Guid fileNameInGuid = Guid.NewGuid();

            try
            {
                if (!string.IsNullOrEmpty(productCode) && !string.IsNullOrEmpty(barCodeText))
                {
                    string fileName = $"{fileNameInGuid}_{productCode.Trim()}.png";
                    string savingPath = Path.Combine(_env.WebRootPath, "itemcodes", "barcodes");

                    if (!Directory.Exists(savingPath))
                    {
                        Directory.CreateDirectory(savingPath);
                    }

                    var barCode = new Barcode
                    {
                        Alignment = AlignmentPositions.Center,
                        IncludeLabel = true,
                        ForeColor = SKColors.Black,
                        BackColor = SKColors.White,
                        Width = 300,
                        Height = 150,
                        LabelFont = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), 28),
                        EncodedType = BarcodeStandard.Type.Ean13
                    };
                    var barCodeImage = barCode.Encode(barCodeText);
                    var filePath = Path.Combine(savingPath, fileName);

                    using (var imageFile = barCodeImage.Encode(SKEncodedImageFormat.Png, 100))
                    using (var fileStream = File.OpenWrite(filePath))
                    {
                        imageFile.SaveTo(fileStream);
                    }

                    returnPath = $"/itemcodes/barcodes/{fileName}";
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework or system diagnostics)
                Console.WriteLine($"Error generating barcode: {ex.Message}");
                returnPath = "";
            }

            return returnPath;
        }
        public bool RemoveBarcode(string barcodeImgPath)
        {
            try
            {
                var fileName = Path.GetFileName(barcodeImgPath);
                var fullImgPath = Path.Combine(_env.WebRootPath, "itemcodes", "barcodes", fileName);
                if (!File.Exists(fullImgPath))
                {
                    return false;
                }

                File.Delete(fullImgPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on File Deleting: {ex.Message}");
                return false;
            }
        }

        public string GenerateQRCode(string productCode, string qrCodeText)
        {
            string returnPath = "";
            Guid fileNameInGuid = Guid.NewGuid();

            try
            {
                if (!string.IsNullOrEmpty(productCode) && !string.IsNullOrEmpty(qrCodeText))
                {
                    string fileName = $"{fileNameInGuid}_{productCode.Trim()}.png";
                    string savingPath = Path.Combine(_env.WebRootPath, "itemcodes", "qrcodes");

                    if (!Directory.Exists(savingPath))
                    {
                        Directory.CreateDirectory(savingPath);
                    }

                    using (var generator = new QRCodeGenerator())
                    {
                        var qrCodeData = generator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                        using (var qrCode = new QRCode(qrCodeData))
                        {
                            using (var qrCodeImage = qrCode.GetGraphic(20))
                            {
                                var filePath = Path.Combine(savingPath, fileName);
                                qrCodeImage.Save(filePath, ImageFormat.Png);
                                returnPath = $"/itemcodes/qrcodes/{fileName}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework or system diagnostics)
                Console.WriteLine($"Error generating qrcode: {ex.Message}");
                returnPath = "";
            }

            return returnPath;
        }
        public bool RemoveQRCode(string qrcodeImgPath)
        {
            try
            {
                var fileName = Path.GetFileName(qrcodeImgPath);
                var fullImgPath = Path.Combine(_env.WebRootPath, "itemcodes", "qrcodes", fileName);
                if (!File.Exists(fullImgPath))
                {
                    return false;
                }

                File.Delete(fullImgPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on File Deleting: {ex.Message}");
                return false;
            }
        }
    }//End of the Service
}//End of the Namespace