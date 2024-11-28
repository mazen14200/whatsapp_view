using WhatsUp.Core;
using WhatsUp.Core.Interfaces;
using WhatsUp.Core.Models;
using WhatsUp.Inferastructure.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Globalization;

namespace WhatsUp.Inferastructure
{
    public static class PersistenceContainer
    {

        public static WebApplicationBuilder AddPersistenceServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddLocalization();
            builder.Services.AddDbContext<WhatsUpContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
                /* options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);*/
            });

            //builder.Services.AddIdentity<CrMasUserInformation, IdentityRole>(opt =>
            //{
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequireLowercase = false;
            //    opt.Password.RequiredLength = 1;
            //}).AddEntityFrameworkStores<WhatsUpContext>()
            //  .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG")
                };
                foreach (var culture in supportedCultures)
                {
                    // Customize the number format for each supported culture
                    culture.NumberFormat.CurrencyDecimalDigits = 2;
                    culture.NumberFormat.CurrencyDecimalSeparator = ".";
                    culture.NumberFormat.CurrencyGroupSeparator = ",";
                }
                options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            });


            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = "localhost,abortConnect=false,connectTimeout=10000";
                opt.InstanceName = "redisUserstat";

            });

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "MySessionCookie";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //if you dont use Jwt i think you can just delete this line
            }).AddCookie(/*cookie=> you can add some options here*/);


            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login";
            //    options.ExpireTimeSpan = TimeSpan.FromSeconds(6);

            //    options.Events = new CookieAuthenticationEvents()
            //    {
            //        OnSigningOut = (context) =>
            //        {
            //            Debug.WriteLine("test");
            //            return Task.CompletedTask;
            //        }
            //    };
            //});


            builder.Services.AddHttpContextAccessor();
   
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            builder.Services.AddScoped<IRenterIdType, RenterIdType>();



            return builder;
        }



    }
}