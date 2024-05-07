using AutoMapper;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class Money_AdminstritiveProceduresController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService ;
        public Money_AdminstritiveProceduresController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IUserLoginsService userLoginsService) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
        }

        public async Task<IActionResult> Money_AdminstritiveProcedures()
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "1";
            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("205", "2205002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var StartDate_Table = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x=>x.CrCasSysAdministrativeProceduresClassification=="30")?.Max(x=>x.CrCasSysAdministrativeProceduresDate);

            var today = DateTime.Today;
            if(StartDate_Table != null)
            {
                today = DateTime.Parse(StartDate_Table?.ToString("yyyy-MM-dd")).Date;
            }
            var startDate = today.AddDays(-30).Date;
            var endDate = today.Date;

            ViewBag.startDate = startDate.ToString("yyyy-MM-dd");
            ViewBag.endDate = endDate.ToString("yyyy-MM-dd");

            var lessorCode = userLogin.CrMasUserInformationLessor;
            DateTime sd = Convert.ToDateTime(startDate);
            DateTime ed = Convert.ToDateTime(endDate);
            var Money_AdminstritiveProcedures = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresClassification == "30" &&
                                                                                                   x.CrCasSysAdministrativeProceduresLessor == userLogin.CrMasUserInformationLessor&&
                                                                                                   x.CrCasSysAdministrativeProceduresDate >= sd && x.CrCasSysAdministrativeProceduresDate <= ed,
                                                                                                    new[] { "CrCasSysAdministrativeProceduresCodeNavigation", "CrCasSysAdministrativeProcedures", "CrCasSysAdministrativeProceduresUserInsertNavigation" })
                                                                                                   .OrderByDescending(x => x.CrCasSysAdministrativeProceduresDate).ThenByDescending(x => x.CrCasSysAdministrativeProceduresTime).ToList();
            var model = _mapper.Map<List<AdminstritiveProceduresVM>>(Money_AdminstritiveProcedures);

            // SaveTracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205002", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            foreach (var item in model)
            {
                if (item.CrCasSysAdministrativeProceduresCode == "303" || item.CrCasSysAdministrativeProceduresCode == "304")
                {
                    var Employee = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Employee != null)
                    {
                        // if 303 304 then it is Employee
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Employee.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Employee.CrMasUserInformationEnName})";

                    }
                }
                else if (item.CrCasSysAdministrativeProceduresCode == "306")
                {
                    var Renter = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Renter != null)
                    {
                        // if 306 then it is Renter
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Renter.CrMasRenterInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Renter.CrMasRenterInformationEnName})";

                    }
                }
                else
                {
                    // if All 30 not upper 3 then it is 
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ";

                }
            };

            return View(model);
        }

        public async Task<PartialViewResult> Money_AdminstritiveProceduresFilterDate(string startDate, string endDate)
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "1";

            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("205", "2205002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            DateTime sd = Convert.ToDateTime(startDate);
            DateTime ed = Convert.ToDateTime(endDate);
            var Money_AdminstritiveProcedures = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresClassification == "30" &&
                                                                                                   x.CrCasSysAdministrativeProceduresLessor == userLogin.CrMasUserInformationLessor &&
                                                                                                   x.CrCasSysAdministrativeProceduresDate>=sd&&x.CrCasSysAdministrativeProceduresDate<=ed,
                                                                                                    new[] { "CrCasSysAdministrativeProceduresCodeNavigation", "CrCasSysAdministrativeProcedures", "CrCasSysAdministrativeProceduresUserInsertNavigation" })
                                                                                                   .OrderByDescending(x => x.CrCasSysAdministrativeProceduresDate).ThenByDescending(x => x.CrCasSysAdministrativeProceduresTime).ToList();
            var lessorCode = userLogin.CrMasUserInformationLessor;

            var model = _mapper.Map<List<AdminstritiveProceduresVM>>(Money_AdminstritiveProcedures);

            foreach (var item in model) 
            {
                if (item.CrCasSysAdministrativeProceduresCode == "303" || item.CrCasSysAdministrativeProceduresCode == "304")
                {
                    var Employee = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Employee != null)
                    {
                        // if 303 304 then it is Employee
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Employee.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Employee.CrMasUserInformationEnName})";

                    }
                }
                else if (item.CrCasSysAdministrativeProceduresCode == "306")
                {
                    var Renter = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Renter != null)
                    {
                        // if 306 then it is Renter
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Renter.CrMasRenterInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Renter.CrMasRenterInformationEnName})";

                    }
                }
                else
                {
                    // if All 30 not upper 3 then it is 
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ";

                }
            };

            return PartialView("_DataTableMoney_AdminstritiveProcedures", model);
        }
    }
}
