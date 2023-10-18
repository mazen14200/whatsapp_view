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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class CarsInformationController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly ICarInformation _carInformation;
        private readonly IStringLocalizer<CarsInformationController> _localizer;
        private readonly IToastNotification _toastNotification;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IDocumentsMaintainanceCar _documentsMaintainanceCar;

        public CarsInformationController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, ICarInformation carInformation, IUserLoginsService userLoginsService, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures, IStringLocalizer<CarsInformationController> localizer, IDocumentsMaintainanceCar documentsMaintainanceCar) : base(userManager, unitOfWork, mapper)
        {
            _carInformation = carInformation;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
            _localizer = localizer;
            _documentsMaintainanceCar = documentsMaintainanceCar;
        }

        public async Task<IActionResult> CarsInformation()
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "0";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var cars = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode && x.CrCasCarInformationStatus == Status.Active, new[] { "CrCasCarInformation1", "CrCasCarInformationDistributionNavigation", "CrCasCarInformationCategoryNavigation" });
            return View(cars);
        }
        [HttpGet]
        public async Task<PartialViewResult> GetCarsByStatus(string status)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            if (userLogin != null)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    var carsAll = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationLessor == lessorCode, new[] { "CrCasCarInformation1", "CrCasCarInformationDistributionNavigation", "CrCasCarInformationCategoryNavigation" });
                    if (status == Status.All)
                    {
                        return PartialView("_DataTableCars", carsAll.Where(x => x.CrCasCarInformationStatus == Status.Active || x.CrCasCarInformationStatus == Status.Hold || x.CrCasCarInformationStatus == Status.Rented));
                    }
                    return PartialView("_DataTableCars", carsAll.Where(x => x.CrCasCarInformationStatus == status));
                }
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> AddCar()
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "0";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Add", titles[3]);
            var CarsDistribution = _unitOfWork.CrMasSupCarDistribution.FindAll(x => x.CrMasSupCarDistributionStatus == Status.Active).ToList();
            ViewData["CarsDistribtionAr"] = CarsDistribution.Select(x => new SelectListItem { Value = x.CrMasSupCarDistributionCode.ToString(), Text = x.CrMasSupCarDistributionConcatenateArName }).ToList();
            ViewData["CarsDistribtionEn"] = CarsDistribution.Select(x => new SelectListItem { Value = x.CrMasSupCarDistributionCode.ToString(), Text = x.CrMasSupCarDistributionConcatenateEnName }).ToList();
            var RegistrationType = _unitOfWork.CrMasSupCarRegistration.FindAll(x => x.CrMasSupCarRegistrationStatus == Status.Active).ToList();
            ViewData["RegistrationTypeAr"] = RegistrationType.Select(x => new SelectListItem { Value = x.CrMasSupCarRegistrationCode.ToString(), Text = x.CrMasSupCarRegistrationArName }).ToList();
            ViewData["RegistrationTypeEn"] = RegistrationType.Select(x => new SelectListItem { Value = x.CrMasSupCarRegistrationCode.ToString(), Text = x.CrMasSupCarRegistrationEnName }).ToList();
            var FuelType = _unitOfWork.CrMasSupCarFuel.FindAll(x => x.CrMasSupCarFuelStatus == Status.Active).ToList();
            ViewData["FuelTypeAr"] = FuelType.Select(x => new SelectListItem { Value = x.CrMasSupCarFuelCode.ToString(), Text = x.CrMasSupCarFuelArName }).ToList();
            ViewData["FuelTypeEn"] = FuelType.Select(x => new SelectListItem { Value = x.CrMasSupCarFuelCode.ToString(), Text = x.CrMasSupCarFuelEnName }).ToList();
            var CVTtype = _unitOfWork.CrMasSupCarCvt.FindAll(x => x.CrMasSupCarCvtStatus == Status.Active).ToList();
            ViewData["CVTtypeAr"] = CVTtype.Select(x => new SelectListItem { Value = x.CrMasSupCarCvtCode.ToString(), Text = x.CrMasSupCarCvtArName }).ToList();
            ViewData["CVTtypeEn"] = CVTtype.Select(x => new SelectListItem { Value = x.CrMasSupCarCvtCode.ToString(), Text = x.CrMasSupCarCvtEnName }).ToList();
            var Colors = _unitOfWork.CrMasSupCarColor.FindAll(x => x.CrMasSupCarColorStatus == Status.Active).ToList();
            ViewData["ColorsAr"] = Colors.Select(x => new SelectListItem { Value = x.CrMasSupCarColorCode.ToString(), Text = x.CrMasSupCarColorArName }).ToList();
            ViewData["ColorsEn"] = Colors.Select(x => new SelectListItem { Value = x.CrMasSupCarColorCode.ToString(), Text = x.CrMasSupCarColorEnName }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarsInforamtionVM carsInforamtionVM, string firstChar, string secondChar, string thirdChar, string BoardNumber)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var CarsDistribution = _unitOfWork.CrMasSupCarDistribution.FindAll(x => x.CrMasSupCarDistributionStatus == Status.Active).ToList();

            // get plate Number 
            var firstEnChar = firstChar[0];
            var secondEnChar = secondChar[0];
            var ThirdEnChar = thirdChar[0];
            string firstArChar; if (firstChar[1].ToString() == "ه") firstArChar = "هـ"; else firstArChar = firstChar[1].ToString();
            string secondArChar; if (secondChar[1].ToString() == "ه") secondArChar = "هـ"; else secondArChar = secondChar[1].ToString();
            string thirdArChar; if (thirdChar[1].ToString() == "ه") thirdArChar = "هـ"; else thirdArChar = thirdChar[1].ToString();
            carsInforamtionVM.CrCasCarInformationPlateArNo = $"{firstArChar} {secondArChar} {thirdArChar} {BoardNumber}";
            carsInforamtionVM.CrCasCarInformationPlateEnNo = $"{firstEnChar} {secondEnChar} {ThirdEnChar} {BoardNumber}";

            var serialNoExist = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo == carsInforamtionVM.CrCasCarInformationSerailNo);
            var SturctureNoExist = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationStructureNo == carsInforamtionVM.CrCasCarInformationStructureNo);
            var PlateNoExist = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationPlateArNo == carsInforamtionVM.CrCasCarInformationPlateArNo ||
                                                                         x.CrCasCarInformationPlateEnNo == carsInforamtionVM.CrCasCarInformationPlateEnNo);

            if (serialNoExist != null) ModelState.AddModelError("CrCasCarInformationSerailNo", _localizer["IsExists"]);
            if (SturctureNoExist != null) ModelState.AddModelError("CrCasCarInformationStructureNo", _localizer["IsExists"]);
            if (PlateNoExist != null)
            {
                ModelState.AddModelError("BoardNumber", _localizer["IsExists"]);
            }
            var DistribtionCode = "";
            var Distriction = _unitOfWork.CrMasSupCarDistribution.Find(x => x.CrMasSupCarDistributionConcatenateArName == carsInforamtionVM.CrCasCarInformationDistribution ||
                                                                            x.CrMasSupCarDistributionConcatenateEnName == carsInforamtionVM.CrCasCarInformationDistribution);
            if (Distriction == null)
            {
                ModelState.AddModelError("CrMasSupCarDistributionConcatenateName", _localizer["RessureFromNameCar"]);
            }
            else
            {
                carsInforamtionVM.CrCasCarInformationDistribution = Distriction.CrMasSupCarDistributionCode;
            }

            if (ModelState.IsValid)
            {
                var model = _mapper.Map<CrCasCarInformation>(carsInforamtionVM);
                model.CrCasCarInformationLessor = lessorCode;
                var carRes = await _carInformation.AddCarInformation(model);
                var docRes = await _documentsMaintainanceCar.AddDocumentCar(model.CrCasCarInformationSerailNo,lessorCode,"100",(int)model.CrCasCarInformationCurrentMeter);
                var mainRes = await _documentsMaintainanceCar.AddMaintainaceCar(model.CrCasCarInformationSerailNo, lessorCode, "100", (int)model.CrCasCarInformationCurrentMeter);
                if (carRes==true&&docRes==true&&mainRes==true)
                {
                    await _unitOfWork.CompleteAsync();
                    // SaveTracing
                    var (mainTask, subTask, system, currentUserr) = await SetTrace("202", "2202001", "2");
                    await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUserr.CrMasUserInformationCode, "1", "202", "20", currentUserr.CrMasUserInformationLessor, "100",
                        model.CrCasCarInformationSerailNo, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("CarsInformation", "CarsInformation");
                }
            }
            var RegistrationType = _unitOfWork.CrMasSupCarRegistration.FindAll(x => x.CrMasSupCarRegistrationStatus == Status.Active).ToList();
            ViewData["RegistrationTypeAr"] = RegistrationType.Select(x => new SelectListItem { Value = x.CrMasSupCarRegistrationCode.ToString(), Text = x.CrMasSupCarRegistrationArName }).ToList();
            ViewData["RegistrationTypeEn"] = RegistrationType.Select(x => new SelectListItem { Value = x.CrMasSupCarRegistrationCode.ToString(), Text = x.CrMasSupCarRegistrationEnName }).ToList();
            var FuelType = _unitOfWork.CrMasSupCarFuel.FindAll(x => x.CrMasSupCarFuelStatus == Status.Active).ToList();
            ViewData["FuelTypeAr"] = FuelType.Select(x => new SelectListItem { Value = x.CrMasSupCarFuelCode.ToString(), Text = x.CrMasSupCarFuelArName }).ToList();
            ViewData["FuelTypeEn"] = FuelType.Select(x => new SelectListItem { Value = x.CrMasSupCarFuelCode.ToString(), Text = x.CrMasSupCarFuelEnName }).ToList();
            var CVTtype = _unitOfWork.CrMasSupCarCvt.FindAll(x => x.CrMasSupCarCvtStatus == Status.Active).ToList();
            ViewData["CVTtypeAr"] = CVTtype.Select(x => new SelectListItem { Value = x.CrMasSupCarCvtCode.ToString(), Text = x.CrMasSupCarCvtArName }).ToList();
            ViewData["CVTtypeEn"] = CVTtype.Select(x => new SelectListItem { Value = x.CrMasSupCarCvtCode.ToString(), Text = x.CrMasSupCarCvtEnName }).ToList();
            var Colors = _unitOfWork.CrMasSupCarColor.FindAll(x => x.CrMasSupCarColorStatus == Status.Active).ToList();
            ViewData["ColorsAr"] = Colors.Select(x => new SelectListItem { Value = x.CrMasSupCarColorCode.ToString(), Text = x.CrMasSupCarColorArName }).ToList();
            ViewData["ColorsEn"] = Colors.Select(x => new SelectListItem { Value = x.CrMasSupCarColorCode.ToString(), Text = x.CrMasSupCarColorEnName }).ToList();
            return View(carsInforamtionVM);
        }

        [HttpGet]
        public JsonResult GetCarsName()
        {
            var carsInformation = _unitOfWork.CrMasSupCarDistribution.GetAll();

            if (CultureInfo.CurrentCulture.Name == "ar-EG")
            {
                var carArrayAr = carsInformation.Select(c => new { text = c.CrMasSupCarDistributionConcatenateArName, value = c.CrMasSupCarDistributionCode });
                return Json(carArrayAr);
            }
            var carArrayEn = carsInformation.Select(c => new { text = c.CrMasSupCarDistributionConcatenateEnName, value = c.CrMasSupCarDistributionCode });
            return Json(carArrayEn);
        }
    }
}
