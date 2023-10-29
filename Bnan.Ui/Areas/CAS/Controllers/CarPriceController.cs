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
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class CarPriceController : BaseController
    {
        public CarPriceController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(userManager, unitOfWork, mapper)
        {
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
            var carPrices = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicLessorCode == lessorCode && x.CrCasPriceCarBasicStatus == Status.Active, new[] { "CrCasPriceCarBasicLessorCodeNavigation", "CrCasPriceCarBasicDistributionCodeNavigation" });
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
                    var carPrices = _unitOfWork.CrCasPriceCarBasic.FindAll(x => x.CrCasPriceCarBasicLessorCode == lessorCode);

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
            return View();
        }
       [HttpGet]
        public async Task<JsonResult> GetNumberOfCar(string DistribtionCode)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var carsInformation = _unitOfWork.CrCasCarInformation.FindAll(x=>x.CrCasCarInformationDistribution==DistribtionCode&&x.CrCasCarInformationLessor== lessorCode).Count();
            return Json(carsInformation);
        }
    }
}
