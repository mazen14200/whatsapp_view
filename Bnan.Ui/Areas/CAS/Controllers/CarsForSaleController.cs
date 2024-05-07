using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class CarsForSaleController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly ICarInformation _carInformation;
        private readonly IStringLocalizer<CarsInformationController> _localizer;
        private readonly IToastNotification _toastNotification;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IDocumentsMaintainanceCar _documentsMaintainanceCar;

        public CarsForSaleController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, ICarInformation carInformation, IUserLoginsService userLoginsService, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures, IStringLocalizer<CarsInformationController> localizer, IDocumentsMaintainanceCar documentsMaintainanceCar) : base(userManager, unitOfWork, mapper)
        {
            _carInformation = carInformation;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
            _localizer = localizer;
            _documentsMaintainanceCar = documentsMaintainanceCar;
        }
        public async Task<IActionResult> CarsForSale()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("202", "2202008", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "7";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202008", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var cars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode &&
                                                                   (x.CrCasCarInformationStatus != Status.Sold && x.CrCasCarInformationStatus != Status.Deleted && x.CrCasCarInformationStatus != Status.Hold) &&
                                                                    x.CrCasCarInformationBranchStatus == Status.Active &&
                                                                    x.CrCasCarInformationOwnerStatus == Status.Active,
                                                                    new[] { "CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                            "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2" });
            return View(cars);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetCarsByStatus(string status)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var lessor = await _unitOfWork.CrMasLessorInformation.FindAsync(x => x.CrMasLessorInformationCode == lessorCode);
            var cars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode &&
                                                                    x.CrCasCarInformationBranchStatus == Status.Active &&
                                                                    x.CrCasCarInformationOwnerStatus == Status.Active,
                                                                    new[] { "CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                            "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2" });

            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All.ToLower()) return PartialView("_CarsForSaleData", cars.Where(x => x.CrCasCarInformationStatus != Status.Deleted && x.CrCasCarInformationStatus != Status.Sold && x.CrCasCarInformationStatus != Status.Hold));
                else if (status != Status.Sold) return PartialView("_CarsForSaleData", cars.Where(x => x.CrCasCarInformationStatus != Status.Deleted && x.CrCasCarInformationStatus != Status.Sold && x.CrCasCarInformationStatus != Status.Hold && x.CrCasCarInformationForSaleStatus==status));
                else return PartialView("_CarsForSaleData", cars.Where(x => x.CrCasCarInformationStatus == status));
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> OfferCarForSale(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "7";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202008", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == id && x.CrCasCarInformationLessor == lessorCode, new[] {"CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                                                                               "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2"});
            ViewBag.OfferDate = car.CrCasCarInformationOfferedSaleDate?.ToString("yyyy/MM/dd");
            ViewBag.SoldDate = car.CrCasCarInformationSoldDate?.ToString("yyyy/MM/dd");
            ViewBag.offerValue = car.CrCasCarInformationOfferValueSale?.ToString("N2", CultureInfo.InvariantCulture);
            if (car.CrCasCarInformationForSaleStatus == Status.RendAndForSale) ViewBag.ForRent = _localizer["Yes"];
            else ViewBag.ForRent = _localizer["Noo"];



            var carVM = _mapper.Map<CarsInforamtionVM>(car);
            return View(carVM);
        }

        [HttpPost]
        public async Task<IActionResult> OfferCarForSale(CarsInforamtionVM carsInforamtionVM)
        {
            var sAr = "";
            var sEn = "";

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            var car = await _unitOfWork.CrCasCarInformation.FindAsync(x => x.CrCasCarInformationSerailNo == carsInforamtionVM.CrCasCarInformationSerailNo && x.CrCasCarInformationLessor == lessorCode,
                                                                                                                        new[] {"CrCasCarInformation1", "CrCasCarInformationDistributionNavigation",
                                                                                                                               "CrCasCarInformationCategoryNavigation", "CrCasCarInformation2"});
            if (car != null)
            {
                var carModel = _mapper.Map<CrCasCarInformation>(carsInforamtionVM);
                if (await _carInformation.UpdateCarToSale(carModel))
                {
                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUserr) = await SetTrace("202", "2202008", "2");
                    await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, "عرض للبيع", "Offer For Sale", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


                    if (carModel.CrCasCarInformationStatus == Status.Active && carModel.CrCasCarInformationForSaleStatus == null)
                    {
                        sAr = "للبيع فقط";
                        sEn = "For Sale";
                    }
                    else if (carModel.CrCasCarInformationStatus == Status.Active && carModel.CrCasCarInformationForSaleStatus == "true")
                    {
                        sAr = "للبيع والإيجار";
                        sEn = "For Sale And Rent";
                    }

                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUserr.CrMasUserInformationCode, "1", "217", "20", currentUserr.CrMasUserInformationLessor, "100",
                        car.CrCasCarInformationSerailNo, null, null, null, null, null, null, null, null, sAr, sEn, "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("CarsForSale");
                };

            }
            return View(carsInforamtionVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditStatusOfSaleCar(string code, string status,string reasons)
        {
            string sAr = "";
            string sEn = "";
            var userLogin = await _userManager.GetUserAsync(User);
            var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == code && x.CrCasCarInformationLessor == userLogin.CrMasUserInformationLessor);
            if (car != null)
            {
                if (status == Status.Save)
                {
                    sAr = "الغاء البيع";
                    sEn = "Cancel offer";
                    car.CrCasCarInformationForSaleStatus = Status.Active;
                    car.CrCasCarInformationOfferValueSale = 0;
                    car.CrCasCarInformationOfferedSaleDate = null;
                    car.CrCasCarInformationSoldDate = null;
                    car.CrCasCarInformationReasons = reasons;
                }
                if (status == Status.Hold)
                {
                    sAr = "تنفيذ البيع";
                    sEn = "Confirm Offer";
                    car.CrCasCarInformationStatus = Status.Sold;
                    car.CrCasCarInformationSoldDate = DateTime.Now.Date;
                    car.CrCasCarInformationReasons = reasons;
                }
                await _unitOfWork.CompleteAsync();
                // SaveTracing
                var (mainTask, subTask, system, currentUserr) = await SetTrace("202", "2202008", "2");
                await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                var proCode = "";
                if (status == Status.Save) proCode = "217";
                else proCode = "218";
                await _adminstritiveProcedures.SaveAdminstritive(currentUserr.CrMasUserInformationCode, "1", proCode, "20", currentUserr.CrMasUserInformationLessor, "100",
                    car.CrCasCarInformationSerailNo, null, null, null, null, null, null, null, null, sAr, sEn, "U", null);
                return RedirectToAction("CarsForSale");
            }
            _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("CarsForSale");
        }






        public IActionResult SuccesssMessageForCarsInformation()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("CarsForSale");
        }
    }
}
