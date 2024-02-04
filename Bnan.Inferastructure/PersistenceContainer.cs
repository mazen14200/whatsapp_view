using Bnan.Core;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Core.Repository;
using Bnan.Inferastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;

namespace Bnan.Inferastructure
{
    public static class PersistenceContainer
    {

        public static WebApplicationBuilder AddPersistenceServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddLocalization();
            builder.Services.AddDbContext<BnanKSAContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
                /* options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);*/
            });

            builder.Services.AddIdentity<CrMasUserInformation, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
            }).AddEntityFrameworkStores<BnanKSAContext>()
              .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
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

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/Account/Login";
                   options.ExpireTimeSpan = TimeSpan.FromSeconds(6);

                   options.Events = new CookieAuthenticationEvents()
                   {
                       OnSigningOut = (context) =>
                       {
                           Debug.WriteLine("test");
                           return Task.CompletedTask;
                       }
                   };
               });


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserLoginsService, UserLoginsService>();
            builder.Services.AddScoped<IUserMainValidtion, UserMainValidtionService>();
            builder.Services.AddScoped<IUserSubValidition, UserSubValiditionService>();
            builder.Services.AddScoped<IUserProcedureValidition, UserProcedureValiditionService>();
            builder.Services.AddScoped<ILessorImage, LessorImage>();
            builder.Services.AddScoped<IOwner, Owner>();
            builder.Services.AddScoped<IBeneficiary, Beneficiary>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ILessorMembership, LessorMembership>();
            builder.Services.AddScoped<ILessorMechanism, LessorMechanism>();
            builder.Services.AddScoped<ICompnayContract, CompnayContract>();
            builder.Services.AddScoped<IBranchInformation, BranchInformation>();
            builder.Services.AddScoped<IBranchDocument, BranchDocument>();
            builder.Services.AddScoped<IPostBranch, PostBranch>();
            builder.Services.AddScoped<IAccountBank, AccountBank>();
            builder.Services.AddScoped<ISalesPoint, SalesPoint>();
            builder.Services.AddScoped<IBankService, BankService>();
            builder.Services.AddScoped<IPaymentMethods, PaymentMethods>();
            builder.Services.AddScoped<IAccountRefrence, AccountRefrence>();
            builder.Services.AddScoped<ICarDistribution, CarDistribution>();

            builder.Services.AddScoped<IContractAdditional, ContractAdditional>();
            builder.Services.AddScoped<IContractOptions, ContractOptions>();
            builder.Services.AddScoped<IPostCity, PostCity>();
            builder.Services.AddScoped<ICarCheckup, CarCheckup>();
            builder.Services.AddScoped<IPostRegion, PostRegion>();

            builder.Services.AddScoped<IUserBranchValidity, UserBranchValidity>();
            builder.Services.AddScoped<IRenterDriver, RenterDriver>();
            builder.Services.AddScoped<IDrivingLicense, DrivingLicense>();

            builder.Services.AddScoped<IRenterIDType, IDType>();

            builder.Services.AddScoped<IRenterGender, RenterGender>();
            builder.Services.AddScoped<IRenterNationality, RenterNationality>();
            builder.Services.AddScoped<IUserContractValididation, UserContractValididation>();
            builder.Services.AddScoped<IAdminstritiveProcedures, AdminstritiveProcedures>();
            builder.Services.AddScoped<ICarInformation, CarInformation>();
            builder.Services.AddScoped<IDocumentsMaintainanceCar, DocumentsMaintainanceCar>();
            builder.Services.AddScoped<ICarPrice, CarPrice>();
            builder.Services.AddScoped<IMembershipConditions, MembershipConditions>();
            builder.Services.AddScoped<IContract, Contract>();
            builder.Services.AddScoped<ICustody, Custody>();
            builder.Services.AddScoped<ITransferToFromRenter, TransferToFromRenter>();
            builder.Services.AddScoped<IContractExtension, ContractExtension>();
            builder.Services.AddScoped<IFeedBoxBS, FeedBoxBS>();

            return builder;
        }



    }
}