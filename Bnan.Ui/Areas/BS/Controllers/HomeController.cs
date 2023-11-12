using AutoMapper;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class HomeController : BaseController
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IStringLocalizer<HomeController> localizer, BnanKSAContext context, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == lessorCode).ToList();
            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches
            };
            return View(bSLayoutVM);
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
