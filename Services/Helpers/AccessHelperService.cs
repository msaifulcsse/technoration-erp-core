using Entities.EntityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository.Context;
using Services.Helpers.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class AccessHelperService : IAccessHelperService
    {
        #region "Declared objects & Constructor"
        private readonly ProjectDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICryptoHelperService _cryptoHelperService;
        private readonly IDateTimeHelperService _dateTimeHelperService;

        public AccessHelperService(ProjectDbContext _db, IHostingEnvironment _hostingEnvironment, ICryptoHelperService _cryptoHelperService, IDateTimeHelperService _dateTimeHelperService)
        {
            this._db = _db;
            this._hostingEnvironment = _hostingEnvironment;
            this._cryptoHelperService = _cryptoHelperService;
            this._dateTimeHelperService = _dateTimeHelperService;
        }
        #endregion

        public bool HasAccess()
        {
            bool hasAccess = false;
            try
            {
                var company = _db.CompanySettings.FirstOrDefault();
                if (company != null && !string.IsNullOrWhiteSpace(company.Attribute10))
                {
                    var effectiveToDateString = _cryptoHelperService.Decrypt(company.Attribute10);
                    if (effectiveToDateString == "Not Set")
                    {
                        hasAccess = true;
                    }
                    else if (!string.IsNullOrWhiteSpace(effectiveToDateString))
                    {
                        //As this provided date format is: mm/dd/yyyy that's why called US Date Format Helper
                        var effectiveToDate = Convert.ToDateTime(_dateTimeHelperService.ConvertUSDateStringToDateTimeObject(effectiveToDateString));
                        if (effectiveToDate > _dateTimeHelperService.Now())
                        {
                            hasAccess = true;
                        }
                    }
                    try
                    {
                        using (StreamReader r = new StreamReader(Path.Combine(_hostingEnvironment.WebRootPath, "/asset/files/ExceptionHandler.jsont")))
                        {
                            string json = r.ReadToEnd();
                            var fileData = JsonConvert.DeserializeObject<VmExceptionHandlerFile>(json);
                            if (int.Parse(_cryptoHelperService.Decrypt(fileData.CRMFailedAttempt)) > 30)
                            {
                                hasAccess = false;
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                else { hasAccess = true; }
            }
            catch (Exception ex)
            { hasAccess = false; }
            return hasAccess;
        }

        public async Task SeedCompanyInfoAsync()
        {
            try
            {
                var company = _db.CompanySettings.FirstOrDefault();
                if (company != null)
                {
                    if (string.IsNullOrWhiteSpace(company.Attribute10))
                    {
                        company.Attribute10 = _cryptoHelperService.Encrypt("Not Set");
                        await _db.SaveChangesAsync();
                    }
                }
                else
                {
                    var obj = new CompanySettings
                    {
                        CompanyName = "Not Set",
                        Attribute10 = _cryptoHelperService.Encrypt("Not Set"),
                        IsCustomerIdEditable = false
                    };
                    _db.CompanySettings.Add(company);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex) { }
        }

        public bool HasSubscription()
        {
            var hasSubscription = false;
            try
            {
                var company = _db.CompanySettings.FirstOrDefault();
                if (company != null && !string.IsNullOrWhiteSpace(company.Attribute10))
                {
                    var effectiveToDateString = _cryptoHelperService.Decrypt(company.Attribute10);
                    if (effectiveToDateString == "Not Set")
                    { hasSubscription = true; }
                    else if (!string.IsNullOrWhiteSpace(effectiveToDateString))
                    {
                        var effectiveToDate = Convert.ToDateTime(_dateTimeHelperService.ConvertUSDateStringToDateTimeObject(effectiveToDateString));
                        if (effectiveToDate > _dateTimeHelperService.Now())
                        { hasSubscription = true; }
                    }
                }
                else
                { hasSubscription = true; }
            }
            catch (Exception ex)
            { hasSubscription = false; }
            return hasSubscription;
        }
    }
}