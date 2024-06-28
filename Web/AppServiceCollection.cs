using System;
using System.Text;
using Services.Helpers;
using Services.DataService;
using Services.EmailService;
using System.Collections.Generic;
using Services.Helpers.Interfaces;
using Services.DataService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Web.Helpers.Interfaces;
using Web.Helpers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Web
{
    public static class AppServiceCollection
    {
        public static IServiceCollection WebProjectServiceManager(this IServiceCollection services)
        {
            //Registered Default Interfaces
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //Registered Applications Custom Interfaces
            services.AddTransient<IDisplayMessageHelper, DisplayMessageHelper>();
            services.AddTransient<IFileUploadHelperService, FileUploadHelperService>();
            services.AddTransient<IProductCodeGeneratorService, ProductCodeGeneratorService>();
            return services;
        }
    }
}
