using System;
using Quartz;
using Services;
using Quartz.Spi;
using Quartz.Impl;
using System.Linq;
using System.Threading;
using System.Reflection;
using Repository.Context;
using Repository.Migrations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Features;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectDbContext>(options => options.UseMySql(Configuration.GetConnectionString("projectconnectionstring")));

            #region "Scheduling JOBs Using Quartz"
            var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            services.AddSingleton(scheduler);
            services.AddHostedService<CustomQuartzHostedService>();
            services.AddSingleton<IJobFactory, CustomQuartzJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<NotificationJob>();
            services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notification Job", "0/10 * * * * ?"));
            services.AddHostedService<NewCustomQuartzHostedService>();
            #endregion

            //Interface or Service Register
            services.ServiceProjectServiceManager().WebProjectServiceManager();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            //Use for cookies
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/Account/Index/";
                options.LoginPath = "/Account/Index";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/PageNotFound";
                    await next();
                }
                if (context.Response.StatusCode == 401)
                {
                    context.Request.Path = "/Error/UnAuthorizeError";
                    await next();
                }
                if (context.Response.StatusCode == 500)
                {
                    context.Request.Path = "/Error/InternalServerError";
                    await next();
                }
            });

            app.UseAuthentication();
            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                //This setting breaks OAuth2 and other cross
                // authontication schems, it elevates the
                //level of cookie security for other types of apps
                MinimumSameSitePolicy = SameSiteMode.Strict
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Account}/{action=Index}/{id?}");
            });
            InitializeOrUpdateDatabase(app);
        }

        private void InitializeOrUpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetService<ProjectDbContext>())
                {
                    DBConfiguration.SeedDefaultRoleUserAndUserRoleData(dbContext);
                    dbContext.Database.Migrate();
                }
            }
        }

    }//End of The Startup Class

    #region "Classes-Methods: Scheduling JOBs Using Quartz"
    public class CustomQuartzHostedService : IHostedService
    {
        private readonly IScheduler _scheduler;
        public CustomQuartzHostedService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduler?.Start(cancellationToken);
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler?.Shutdown(cancellationToken);
        }
    }

    [DisallowConcurrentExecution]
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        public NotificationJob(ILogger<NotificationJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }

    public class CustomQuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public CustomQuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle triggerFiredBundle,
        IScheduler scheduler)
        {
            var jobDetail = triggerFiredBundle.JobDetail;
            return (IJob)_serviceProvider.GetService(jobDetail.JobType);
        }
        public void ReturnJob(IJob job) { }
    }

    public class JobMetadata
    {
        public Guid JobId { get; set; }
        public Type JobType { get; }
        public string JobName { get; }
        public string CronExpression { get; }
        public JobMetadata(Guid Id, Type jobType, string jobName,
        string cronExpression)
        {
            JobId = Id;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
    }

    public class NewCustomQuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;
        private readonly JobMetadata jobMetadata;
        public NewCustomQuartzHostedService(ISchedulerFactory
            schedulerFactory,
            JobMetadata jobMetadata,
            IJobFactory jobFactory)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobMetadata = jobMetadata;
            this.jobFactory = jobFactory;
        }
        public IScheduler Scheduler { get; set; }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            var job = CreateJob(jobMetadata);
            var trigger = CreateTrigger(jobMetadata);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithCronSchedule(jobMetadata.CronExpression)
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }
        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder
            .Create(jobMetadata.JobType)
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }
    }
    #endregion

}//End of The Namespace