namespace EnergySystem.Web
{
    using System.Reflection;

    using EnergySystem.Data;
    using EnergySystem.Data.Common;
    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;
    using EnergySystem.Data.Repositories;
    using EnergySystem.Data.Seeding;
    using EnergySystem.Services.Mapping;
    using EnergySystem.Services.Messaging;
    using EnergySystem.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Services.Background;
    using Services.Background.GridPriceEntry;

    using EnergySystem.Services.Data.Report;

    using Hubs;

    using Services.Battery;
    using Services.Data.MarketPrice;
    using Services.Grid;
    using Services.GridPriceEntry;
    using Services.Property;
    using Services.Report;
    using Services.User;
    using Microsoft.AspNetCore.Hosting.StaticWebAssets;

    using OpenQA.Selenium.BiDi.Modules.Script;

    using Profiles;

    using Services.WorkerService;


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
            ConfigureServices(builder.Services, builder.Configuration, builder);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(
            IServiceCollection services,
            IConfiguration configuration,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => {
                options.SignIn.RequireConfirmedAccount = false;

                // builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireDigit =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
                options.Password.RequireNonAlphanumeric =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequireUppercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireLowercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequiredLength =
                    builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
            options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/AccessDenied"; // Change this to your preferred page
                options.LoginPath = "/User/Login";
            });
            services.AddControllersWithViews(
                options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddRazorRuntimeCompilation();

            services.AddHttpClient();

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);
            
            services.AddSignalR();

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(currentAssembly);
            services.AddAutoMapper(typeof(MarketPriceProfile));
            services.AddAutoMapper(typeof(PropertyProfile));
            services.AddAutoMapper(typeof(BatteryProfile));
            services.AddAutoMapper(typeof(GridProfile));


            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddScoped<IPropertyService, PropertyService>();

            services.AddScoped<IGridService, GridService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IBatteryService, BatteryService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IGridPriceEntryService, GridPriceEntryService>();
            services.AddScoped<IMarketPriceService, MarketPriceService>();
            services.AddScoped<IMarketPricesWebScraperService, MarketPricesWebScraperService>();
            services.AddScoped<IUserService, UserService>();

            // Background services
            services.AddSingleton<IHostedService, MarketPricesScraperBackgroundService>();
            services.AddSingleton<IHostedService, ReportGenerationBackgroundService>();
            //services.AddSingleton<IHostedService, GridSupplyPriceService>();
            services.AddHostedService<GridSupplyPriceService>();
            //services.AddHostedService<Worker>();

        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter()
                    .GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapControllers();
            app.MapRazorPages();
            app.MapHub<PowerPanelHub>("/powerpanelhub");
        }
    }
}
