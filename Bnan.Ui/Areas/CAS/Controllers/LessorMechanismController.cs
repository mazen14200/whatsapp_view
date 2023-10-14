using AutoMapper;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Bnan.Ui.ViewModels.CAS.MecanismInputs;
using Bnan.Ui.ViewModels.MAS.UserValiditySystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class LessorMechanismController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<LessorMechanismController> _localizer;
        public LessorMechanismController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toastNotification, IStringLocalizer<LessorMechanismController> localizer) : base(userManager, unitOfWork, mapper)
        {
            _toastNotification = toastNotification;
            _localizer = localizer;
        }

        public async Task<IActionResult> Mechanism()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            var titles = await setTitle("201", "2201005", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var lessorMechanisms = _unitOfWork.CrCasLessorMechanism.FindAll(x => x.CrCasLessorMechanismCode == currentUser.CrMasUserInformationLessor,
                new[] { "CrCasLessorMechanismCodeNavigation", "CrCasLessorMechanismProceduresNavigation" });
            var model = _mapper.Map<List<MechanismVM>>(lessorMechanisms);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditMechanism([FromBody] CheckAndInputsModels model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var lessorMechanisms = _unitOfWork.CrCasLessorMechanism.FindAll(x => x.CrCasLessorMechanismCode == currentUser.CrMasUserInformationLessor,
                new[] { "CrCasLessorMechanismCodeNavigation", "CrCasLessorMechanismProceduresNavigation" });
            foreach (var item in model.ContractValiditions)
            {
                var id = item.id;
                var checkBox = item.valueCheckBox;
                var days = item.Days;
                var mechanism = lessorMechanisms.FirstOrDefault(x => x.CrCasLessorMechanismProcedures == id);
                if (mechanism != null)
                {
                    mechanism.CrCasLessorMechanismActivate = checkBox;
                    mechanism.CrCasLessorMechanismDaysAlertAboutExpire = int.Parse(days);
                    _unitOfWork.CrCasLessorMechanism.Update(mechanism);
                }
            }
            foreach (var item in model.CarsDocuments)
            {
                var id = item.id;
                var checkBox = item.valueCheckBox;
                var days = item.Days;
                var mechanism = lessorMechanisms.FirstOrDefault(x => x.CrCasLessorMechanismProcedures == id);
                if (mechanism != null)
                {
                    mechanism.CrCasLessorMechanismActivate = checkBox;
                    mechanism.CrCasLessorMechanismDaysAlertAboutExpire = int.Parse(days);
                    _unitOfWork.CrCasLessorMechanism.Update(mechanism);
                }
            }
            foreach (var item in model.CarsFixed)
            {
                var id = item.id;
                var checkBox = item.valueCheckBox;
                var days = item.Days;
                var mechanism = lessorMechanisms.FirstOrDefault(x => x.CrCasLessorMechanismProcedures == id);
                if (mechanism != null)
                {
                    mechanism.CrCasLessorMechanismActivate = checkBox;
                    mechanism.CrCasLessorMechanismDaysAlertAboutExpire = int.Parse(days);
                    _unitOfWork.CrCasLessorMechanism.Update(mechanism);
                }
            }
            foreach (var item in model.RenterAndDriver)
            {
                var id = item.id;
                var checkBox = item.valueCheckBox;
                var days = item.Days;
                var km = item.Km;
                var mechanism = lessorMechanisms.FirstOrDefault(x => x.CrCasLessorMechanismProcedures == id);
                if (mechanism != null)
                {
                    mechanism.CrCasLessorMechanismActivate = checkBox;
                    mechanism.CrCasLessorMechanismDaysAlertAboutExpire = int.Parse(days);
                    mechanism.CrCasLessorMechanismKmAlertAboutExpire = int.Parse(km);
                    _unitOfWork.CrCasLessorMechanism.Update(mechanism);
                }
            }
            foreach (var item in model.Additional)
            {
                var id = item.id;
                var checkBox = item.valueCheckBox;
                var mechanism = lessorMechanisms.FirstOrDefault(x => x.CrCasLessorMechanismProcedures == id);
                if (mechanism != null)
                {
                    mechanism.CrCasLessorMechanismActivate = checkBox;
                    _unitOfWork.CrCasLessorMechanism.Update(mechanism);
                }
            }
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }
        public  IActionResult SuccessToast()
        {
           _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Mechanism", "LessorMechanism");
        }
    }
}
