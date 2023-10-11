
using AutoMapper;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
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
            //To Set Title 
            var titles = await setTitle("206", "2206001", "2");
            await ViewData.SetPageTitleAsync(titles[0], "", "", "", "", titles[3]);
            var value = HttpContext.Session.GetString("MyKey");
            ViewBag.t = _localizer["welcome"];
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
