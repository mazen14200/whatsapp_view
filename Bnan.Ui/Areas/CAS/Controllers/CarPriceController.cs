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
using Newtonsoft.Json;
using NToastNotify;
using NToastNotify.Helpers;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class CarPriceController : BaseController
    {
        private readonly IStringLocalizer<CarsInformationController> _localizer;
        private readonly IToastNotification _toastNotification;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly ICarPrice _carPrice;
        private readonly IUserLoginsService _userLoginsService;

        public CarPriceController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, ICarPrice carPrice, IAdminstritiveProcedures adminstritiveProcedures, IToastNotification toastNotification, IStringLocalizer<CarsInformationController> localizer, IUserLoginsService userLoginsService) : base(userManager, unitOfWork, mapper)
        {
            _carPrice = carPrice;
            _adminstritiveProcedures = adminstritiveProcedures;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _userLoginsService = userLoginsService;
        }

        public async Task<IActionResult> CarPrice()
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "6";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202007", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var carPrices = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicLessorCode == lessorCode && x.CrCasPriceCarBasicStatus == Status.Active,
                                                                        new[] { "CrCasPriceCarBasicLessorCodeNavigation", "CrCasPriceCarBasicDistributionCodeNavigation" }).DistinctBy(x => x.CrCasPriceCarBasicDistributionCode)
;
            return View(carPrices);
        }
        [HttpGet]
        public async Task<PartialViewResult> CarPriceByStatus(string status)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;

            if (userLogin != null)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    var carPrices = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicLessorCode == lessorCode,
                                                                           new[] { "CrCasPriceCarBasicLessorCodeNavigation", "CrCasPriceCarBasicDistributionCodeNavigation" }).DistinctBy(x => x.CrCasPriceCarBasicDistributionCode);

                    if (status == Status.All)
                    {
                        return PartialView("_DataTableCarsPrice", carPrices.Where(x => x.CrCasPriceCarBasicStatus != Status.Deleted));
                    }
                    return PartialView("_DataTableCarsPrice", carPrices.Where(x => x.CrCasPriceCarBasicStatus == status));
                }
            }
            return PartialView();
        }

        public async Task<IActionResult> AddPriceCar()
        {
            //sidebar Active
            ViewBag.id = "#sidebarcars";
            ViewBag.no = "6";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("202", "2202007", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Add", titles[3]);
            ViewBag.LessorCode = lessorCode;
            ViewBag.Options=_unitOfWork.CrMasSupContractOption.FindAll(x=>x.CrMasSupContractOptionsStatus== Status.Active);
            ViewBag.Additional = _unitOfWork.CrMasSupContractAdditional.FindAll(x=>x.CrMasSupContractAdditionalStatus== Status.Active);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPriceCar(CarPriceVM carPriceVM,bool CrCasPriceCarBasicIsAdditionalDriver, List<string> Additionals, List<string> Choises, List<string> Features)
        {
           
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var distribution = _unitOfWork.CrMasSupCarDistribution.Find(x => x.CrMasSupCarDistributionConcatenateArName == carPriceVM.CrCasPriceCarBasicDistributionCode ||
                                                                             x.CrMasSupCarDistributionConcatenateEnName == carPriceVM.CrCasPriceCarBasicDistributionCode);

            if (distribution != null) { 
                var carPrice = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicDistributionCode == distribution.CrMasSupCarDistributionCode).Count();
                if (carPrice > 0) ModelState.AddModelError("CrCasPriceCarBasicDistributionCode", _localizer["IsExists"]);
            }
            else ModelState.AddModelError("CrCasPriceCarBasicDistributionCode", _localizer["RessureFromNameCar"]);
            
            if (ModelState.IsValid)
            {
                var carsInformation = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationDistribution == distribution.CrMasSupCarDistributionCode && x.CrCasCarInformationLessor == lessorCode);
                carPriceVM.CrCasPriceCarBasicIsAdditionalDriver = CrCasPriceCarBasicIsAdditionalDriver;
                carPriceVM.CrCasPriceCarBasicDistributionCode = distribution.CrMasSupCarDistributionCode;
                foreach (var car in carsInformation)
                {
                    carPriceVM.CrCasPriceCarBasicNo = car.CrCasCarInformationSerailNo;
                    if (Additionals != null)
                    {
                        List<CarPriceAdditionalStringData> additionalStringData = new List<CarPriceAdditionalStringData>();
                        // Deserialize and filter the checkbox data
                        foreach (var item in Additionals)
                        {
                            List<CarPriceAdditionalStringData> deserializedData = JsonConvert.DeserializeObject<List<CarPriceAdditionalStringData>>(item);
                            additionalStringData.AddRange(deserializedData);
                        }
                        foreach (var item in additionalStringData)
                        {
                            await _carPrice.AddAdditionals(carPriceVM.CrCasPriceCarBasicNo, item.Id, item.Value);
                        }
                    }
                    if (Choises != null)
                    {
                        List<CarPriceChoisesStringData> ChoisesStringData = new List<CarPriceChoisesStringData>();
                        // Deserialize and filter the checkbox data
                        foreach (var item in Choises)
                        {
                            List<CarPriceChoisesStringData> deserializedData = JsonConvert.DeserializeObject<List<CarPriceChoisesStringData>>(item);
                            ChoisesStringData.AddRange(deserializedData);
                        }
                        foreach (var item in ChoisesStringData)
                        {
                            await _carPrice.AddChoises(carPriceVM.CrCasPriceCarBasicNo, item.Id, item.Value);
                        }
                    }
                    if (Features != null)
                    {
                        List<CarPriceFeaturesStringData> ChoisesStringData = new List<CarPriceFeaturesStringData>();
                        // Deserialize and filter the checkbox data
                        foreach (var item in Features)
                        {
                            List<CarPriceFeaturesStringData> deserializedData = JsonConvert.DeserializeObject<List<CarPriceFeaturesStringData>>(item);
                            ChoisesStringData.AddRange(deserializedData);
                        }
                        foreach (var item in ChoisesStringData)
                        {
                            await _carPrice.AddFeatures(carPriceVM.CrCasPriceCarBasicNo, item.Id, item.Value);
                        }
                    }
                    var CarPriceModel = _mapper.Map<CrCasPriceCarBasic>(carPriceVM);
                    await _carPrice.AddPriceCar(CarPriceModel);
                }
                if (await _unitOfWork.CompleteAsync()>0)
                {
                    // SaveTracing
                    var (mainTask, subTask, system, currentUserr) = await SetTrace("202", "2202007", "2");
                    await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUserr.CrMasUserInformationCode, "1", "219", "20", currentUserr.CrMasUserInformationLessor, "100",
                        distribution.CrMasSupCarDistributionCode, null, null, null, null, null, null, null, null, "اضافة", "Insert", "I", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("CarPrice", "CarPrice");
                }
            }
            ViewBag.LessorCode = lessorCode;
            ViewBag.Options = _unitOfWork.CrMasSupContractOption.FindAll(x => x.CrMasSupContractOptionsStatus == Status.Active);
            ViewBag.Additional = _unitOfWork.CrMasSupContractAdditional.FindAll(x => x.CrMasSupContractAdditionalStatus == Status.Active);
            return View(carPriceVM);
        }


        [HttpGet]
        public async Task<JsonResult> GetNumberOfCar(string DistribtionCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var carsInformation = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationDistribution == DistribtionCode && x.CrCasCarInformationLessor == lessorCode, new[] { "CrCasCarAdvantages" });
            return Json(carsInformation.Count());
        }

        [HttpGet]
        public async Task<PartialViewResult> GetAdvantagesOfCar(string DistribtionCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var carInformation = _unitOfWork.CrCasCarInformation.FindAll(x => x.CrCasCarInformationDistribution == DistribtionCode && x.CrCasCarInformationLessor == lessorCode).FirstOrDefault();
            var AdvantagesCars = _unitOfWork.CrCasCarAdvantage.FindAll(x => x.CrCasCarAdvantagesSerialNo == carInformation.CrCasCarInformationSerailNo &&
                                                                            x.CrCasCarAdvantagesCategory == carInformation.CrCasCarInformationCategory &&
                                                                            x.CrCasCarAdvantagesModel == carInformation.CrCasCarInformationModel &&
                                                                            x.CrCasCarAdvantagesCarYear == carInformation.CrCasCarInformationYear &&
                                                                            x.CrCasCarAdvantagesBrand == carInformation.CrCasCarInformationBrand, new[] { "CrCasCarAdvantagesCodeNavigation" });

            return PartialView("_AdvantagesPartialView", AdvantagesCars.ToList());
        }

    }
}
