using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class CarsInformationController : BaseController
    {
        public CarsInformationController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(userManager, unitOfWork, mapper)
        {
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
                        return PartialView("_DataTableCars", carsAll);
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
            var RegistrationType= _unitOfWork.CrMasSupCarRegistration.FindAll(x=>x.CrMasSupCarRegistrationStatus== Status.Active).ToList();
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
    }
}
