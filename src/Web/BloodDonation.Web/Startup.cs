namespace BloodDonation.Web
{
    using System;
    using System.Reflection;

    using BloodDonation.Data;
    using BloodDonation.Data.Common;
    using BloodDonation.Data.Common.Repositories;
    using BloodDonation.Data.Models;
    using BloodDonation.Data.Repositories;
    using BloodDonation.Data.Seeding;
    using BloodDonation.Services.Data.Administator;
    using BloodDonation.Services.Data.Appointment;
    using BloodDonation.Services.Data.Cloudinary;
    using BloodDonation.Services.Data.Donor;
    using BloodDonation.Services.Data.Email;
    using BloodDonation.Services.Data.Home;
    using BloodDonation.Services.Data.Recipient;
    using BloodDonation.Services.Data.Settings;
    using BloodDonation.Services.Data.User;
    using BloodDonation.Services.Data.ViewRenderService;
    using BloodDonation.Services.Mapping;
    using BloodDonation.Services.Messaging;
    using BloodDonation.Web.Infrastructure.Filters;
    using BloodDonation.Web.ViewModels;
    using CloudinaryDotNet;
    using CronJobs;
    using Hangfire;
    using Hangfire.SqlServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BloodDonationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<BloodDonationDbContext>();

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSqlServerStorage(
                this.configuration.GetConnectionString("DefaultConnection"),
                new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true,
                });
            });

            var cloudinaryCredentials = new Account(
                    this.configuration["Cloudinary:CloudName"],
                    this.configuration["Cloudinary:APIKey"],
                    this.configuration["Cloudinary:APISecret"]);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "AdminUserWithState",
                    policy => policy.RequireRole("Admin")
                    .RequireClaim("state"));
            });
            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = this.configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = this.configuration["Authentication:Google:ClientSecret"];
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = this.configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = this.configuration["Authentication:Facebook:AppSecret"];
                    facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
                });

            services.AddSingleton(cloudinaryUtility);
            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IAdministratorService, AdministratorService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRecipientsService, RecipientsService>();
            services.AddTransient<IDonorsService, DonorsService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IAppointmentsService, AppointmentsService>();
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<BloodDonationDbContext>();
                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder(this.configuration).SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                this.SeedHangfireJobs(recurringJobManager, dbContext);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithRedirects("Home/StatusCodeError?errorCode={0}");
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("Home/StatusCodeError?errorCode={0}");
                app.UseExceptionHandler("/Home/StatusCodeError");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseResponseCaching();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/Administrator/Statistic", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() },
            });
            app.UseHangfireServer();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute(name: "AppointmentById", pattern: "/Appointment/AppointmentById/{id}/{information}", defaults: new { controller = "Appointment", action = "All" });
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }

        private void SeedHangfireJobs(IRecurringJobManager recurringJobManager, BloodDonationDbContext dbContext)
        {
            recurringJobManager.AddOrUpdate<DeletedAppointments>("DeletedAppointments", x => x.Work(), Cron.Daily);
        }
    }
}
