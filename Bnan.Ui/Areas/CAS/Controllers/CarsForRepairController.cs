using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class CarsForRepairController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly ICarInformation _carInformation;
        private readonly IStringLocalizer<CarsInformationController> _localizer;
        private readonly IToastNotification _toastNotification;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IDocumentsMaintainanceCar _documentsMaintainanceCar;

        public CarsForRepairController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, ICarInformation carInformation, IUserLoginsService userLoginsService, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures, IStringLocalizer<CarsInformationController> localizer, IDocumentsMaintainanceCar documentsMaintainanceCar) : base(userManager, unitOfWork, mapper)
        {
            _carInformation = carInformation;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
            _localizer = localizer;
            _documentsMaintainanceCar = documentsMaintainanceCar;
        }
        public async Task<IActionResult> CarsForRepair()
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "3";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var cars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode &&
                                                                  (x.CrCasCarInformationStatus != Status.Rented && x.CrCasCarInformationStatus != Status.Sold && x.CrCasCarInformationStatus != Status.Deleted) &&
                                                                    x.CrCasCarInformationPriceStatus == true && x.CrCasCarInformationBranchStatus == Status.Active &&
                                                                    x.CrCasCarInformationOwnerStatus == Status.Active,
                                                                    new[] { "CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                            "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2" });
            return View(cars);
        }


        [HttpGet]
        public async Task<IActionResult> Repair(string serialNumber)
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "3";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == serialNumber && x.CrCasCarInformationLessor == lessorCode, new[] {"CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                                                                               "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2"});
            var adminstritive = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresCode == "214" && x.CrCasSysAdministrativeProceduresLessor == car.CrCasCarInformationLessor &&
                                                                                      x.CrCasSysAdministrativeProceduresBranch == car.CrCasCarInformationBranch && x.CrCasSysAdministrativeProceduresTargeted == car.CrCasCarInformationSerailNo).LastOrDefault();

            ViewBag.date = adminstritive.CrCasSysAdministrativeProceduresDate?.ToString("dd/MM/yyyy");
            ViewBag.reasons = adminstritive.CrCasSysAdministrativeProceduresReasons;

            var carVM = _mapper.Map<CarsInforamtionVM>(car);
            return View(carVM);
        }

        [HttpPost]
        public async Task<IActionResult> Repair(CarsInforamtionVM carsInforamtionVM, string RepairDate)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == carsInforamtionVM.CrCasCarInformationSerailNo && x.CrCasCarInformationLessor == lessorCode,
                                                                                                                        new[] {"CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                                                                               "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2"});
            var date = DateTime.Parse(RepairDate);
            if (car != null && date != null)
            {
                await _adminstritiveProcedures.SaveAdminstritiveForRepairCar(userLogin.CrMasUserInformationCode, lessorCode, car.CrCasCarInformationBranch,
                                                                            car.CrCasCarInformationSerailNo, date, "اضافة", "Insert", "I",
                                                                            carsInforamtionVM.CrCasCarInformationReasons);
                car.CrCasCarInformationStatus = Status.Maintaince;
                _unitOfWork.CrCasCarInformation.Update(car);
                await _unitOfWork.CompleteAsync();
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("CarsForRepair");
            }
            return View(carsInforamtionVM);
        }

        public async Task<IActionResult> EditStatusOfRepairCar(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var userLogin = await _userManager.GetUserAsync(User);
            var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == code&&x.CrCasCarInformationLessor==userLogin.CrMasUserInformationLessor);
            if (car != null)
            {
                if (status == Status.Active)
                {
                    sAr = "استرجاع";
                    sEn = "Retrive";
                    car.CrCasCarInformationStatus = Status.Active;
                }
                if (status == Status.Deleted)
                {
                    sAr = "حذف";
                    sEn = "Delete";
                    car.CrCasCarInformationStatus = Status.Active;
                }
                await _unitOfWork.CompleteAsync();
                // SaveTracing
                var (mainTask, subTask, system, currentUserr) = await SetTrace("202", "2202004", "2");
                await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUserr.CrMasUserInformationCode, "1", "214", "20", currentUserr.CrMasUserInformationLessor, "100",
                    car.CrCasCarInformationSerailNo, null, null, null, null, null, null, null, null, "تعديل", "Edit", "U", null);
                return RedirectToAction("CarsForRepair");
            }
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("CarsForRepair");
        }






        public IActionResult SuccesssMessageForCarsInformation()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("CarsForRepair");
        }

    }
}
