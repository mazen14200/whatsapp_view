using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ContractExtensionController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ContractExtensionController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;

        public ContractExtensionController(IStringLocalizer<ContractExtensionController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501002", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            return View(bsLayoutVM);
        }
    }
}
