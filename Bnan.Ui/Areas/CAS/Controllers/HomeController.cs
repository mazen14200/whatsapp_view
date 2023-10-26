
using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System.Data;
using System.Linq;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
/*    [ServiceFilter(typeof(SessionAuthorizationFilter))]*/
    public class HomeController : BaseController
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        public BnanKSAContext? _context;

        public HomeController(IStringLocalizer<HomeController> localizer, BnanKSAContext context,UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            //To Set Title 
            var titles = await setTitle("206", "2206001", "2");
            await ViewData.SetPageTitleAsync(titles[0], "", "", "", "", titles[3]);
            var value = HttpContext.Session.GetString("MyKey");
            ViewBag.t = _localizer["welcome"];
           
            var CompanyContract = _unitOfWork.CrMasContractCompany.FindAll(x => x.CrMasContractCompanyLessor == lessorCode).ToList();
            ViewBag.CompanyContractRenewed = CompanyContract.Where(x => x.CrMasContractCompanyStatus == Status.Renewed).Count(); 
            ViewBag.CompanyContractAboutExpire = CompanyContract.Where(x => x.CrMasContractCompanyStatus == Status.AboutToExpire).Count();
            ViewBag.CompanyContractExpired = CompanyContract.Where(x => x.CrMasContractCompanyStatus == Status.Expire).Count();

            var Documents = _unitOfWork.CrCasBranchDocument.FindAll(x => x.CrCasBranchDocumentsLessor == lessorCode).ToList();
            ViewBag.CompanyDocumentsRenewed = Documents.Where(x => x.CrCasBranchDocumentsStatus == Status.Renewed).Count();
            ViewBag.CompanyDocumentsAboutExpire = Documents.Where(x => x.CrCasBranchDocumentsStatus == Status.AboutToExpire).Count();
            ViewBag.CompanyDocumentsExpired = Documents.Where(x => x.CrCasBranchDocumentsStatus == Status.Expire).Count();

            var DocumentsCar = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceLessor == lessorCode && x.CrCasCarDocumentsMaintenanceProceduresClassification == "12").ToList();
            ViewBag.CompanyDocumentsCarRenewed = DocumentsCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Renewed).Count();
            ViewBag.CompanyDocumentsCarAboutExpire = DocumentsCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.AboutToExpire).Count();
            ViewBag.CompanyDocumentsCarExpired = DocumentsCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Expire).Count();

            var MaintainceCar = _unitOfWork.CrCasCarDocumentsMaintenance.FindAll(x => x.CrCasCarDocumentsMaintenanceLessor == lessorCode && x.CrCasCarDocumentsMaintenanceProceduresClassification == "13").ToList();
            ViewBag.CompanyMaintainceCarRenewed = MaintainceCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Renewed).Count();
            ViewBag.CompanyMaintainceCarAboutExpire = MaintainceCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.AboutToExpire).Count();
            ViewBag.CompanyMaintainceCarExpired = MaintainceCar.Where(x => x.CrCasCarDocumentsMaintenanceStatus == Status.Expire).Count();

            var PriceCar = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicLessorCode == lessorCode).ToList();
            ViewBag.PriceCarRenewed = PriceCar.Where(x => x.CrCasPriceCarBasicStatus == Status.Renewed).Count();
            ViewBag.PriceCarAboutExpire = PriceCar.Where(x => x.CrCasPriceCarBasicStatus == Status.AboutToExpire).Count();
            ViewBag.PriceCarExpired = PriceCar.Where(x => x.CrCasPriceCarBasicStatus == Status.Expire).Count();

            return View();
        }
       

        [HttpGet]
        public IActionResult SetLanguage(string returnUrl, string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }


    }
}
