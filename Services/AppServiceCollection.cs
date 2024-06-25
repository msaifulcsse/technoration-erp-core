using System;
using System.Text;
using Services.Helpers;
using Services.DataService;
using Services.EmailService;
using System.Collections.Generic;
using Services.Helpers.Interfaces;
using Services.DataService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class AppServiceCollection
    {
        public static IServiceCollection ServiceProjectServiceManager(this IServiceCollection services)
        {
            services.AddTransient<IEmailerService, EmailerService>();
            services.AddTransient<IAccessHelperService, AccessHelperService>();
            services.AddTransient<ICookieHelperService, CookieHelperService>();
            services.AddTransient<ICryptoHelperService, CryptoHelperService>();
            services.AddTransient<IDateTimeHelperService, DateTimeHelperService>();
            services.AddTransient<IPermissionHelperService, PermissionHelperService>();
            services.AddTransient<IValidationHelperService, ValidationHelperService>();
            services.AddTransient<ICredentialManagerService, CredentialManagerService>();
            return services;
        }
    }
}
