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

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class AdminstritiveProceduresController : BaseController
    {
        public AdminstritiveProceduresController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(userManager, unitOfWork, mapper)
        {
        }

        public async Task<IActionResult> AdminstritiveProcedures()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("205", "2205001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var today = DateTime.Today;
            var startDate = today.AddDays(-30).Date;
            var endDate = today.Date;
            var lessorCode = userLogin.CrMasUserInformationLessor;
            DateTime sd = Convert.ToDateTime(startDate);
            DateTime ed = Convert.ToDateTime(endDate);
            var AdminstritiveProcedures = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresClassification != "30" && x.CrCasSysAdministrativeProceduresClassification != "40" &&
                                                                                                   x.CrCasSysAdministrativeProceduresLessor == userLogin.CrMasUserInformationLessor&&
                                                                                                   x.CrCasSysAdministrativeProceduresDate >= sd && x.CrCasSysAdministrativeProceduresDate <= ed,
                                                                                                    new[] { "CrCasSysAdministrativeProceduresCodeNavigation", "CrCasSysAdministrativeProcedures", "CrCasSysAdministrativeProceduresUserInsertNavigation" })
                                                                                                   .OrderByDescending(x => x.CrCasSysAdministrativeProceduresDate).ThenByDescending(x => x.CrCasSysAdministrativeProceduresTime).ToList();
            var model = _mapper.Map<List<AdminstritiveProceduresVM>>(AdminstritiveProcedures);

            foreach (var item in model)
            {
                if (item.CrCasSysAdministrativeProceduresCode == "201")
                {
                    // if 201 then it is branch
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationArShortName})";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationEnShortName})";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "202")
                {
                    // if 202 then it is Doc of branch
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationArShortName})";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationEnShortName})";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "203")
                {
                    // if 202 then it is Contract Company
                    var Contract = _unitOfWork.CrMasContractCompany.Find(x => x.CrMasContractCompanyNo == item.CrCasSysAdministrativeProceduresTargeted && x.CrMasContractCompanyLessor == lessorCode, new[] { "CrMasContractCompanyProceduresNavigation" });
                    if (Contract!=null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Contract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Contract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresEnName})";
                    }
                    
                }
                if (item.CrCasSysAdministrativeProceduresCode == "204")
                {
                    // if 204 then it is Owners
                    var owner = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode == item.CrCasSysAdministrativeProceduresTargeted && x.CrCasOwnersLessorCode == lessorCode);
                    if (owner != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({owner.CrCasOwnersArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({owner.CrCasOwnersEnName})";
                    }
                   
                }
                if (item.CrCasSysAdministrativeProceduresCode == "205")
                {
                    // if 204 then it is Benficity
                    var Benficity = _unitOfWork.CrCasBeneficiary.Find(x => x.CrCasBeneficiaryCode == item.CrCasSysAdministrativeProceduresTargeted && x.CrCasBeneficiaryLessorCode == lessorCode);
                    if (Benficity != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Benficity.CrCasBeneficiaryArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Benficity.CrCasBeneficiaryEnName})";
                    }
                  
                }

                if (item.CrCasSysAdministrativeProceduresCode == "231")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted && x.CrMasUserInformationLessor == lessorCode);
                    if (user != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";

                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "232")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted && x.CrMasUserInformationLessor == lessorCode);
                    if (user != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";

                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "233")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted && x.CrMasUserInformationLessor == lessorCode);
                    if (user != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";

                    }
                  
                }
                if (item.CrCasSysAdministrativeProceduresCode == "234")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (user != null) {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";
                    }
                    
                }
                if (item.CrCasSysAdministrativeProceduresCode == "246")
                {
                    // if 246 then it is Driver
                    var Driver = _unitOfWork.CrCasRenterPrivateDriverInformation.Find(x => x.CrCasRenterPrivateDriverInformationId == item.CrCasSysAdministrativeProceduresTargeted&&x.CrCasRenterPrivateDriverInformationLessor== lessorCode);
                    if (Driver != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Driver.CrCasRenterPrivateDriverInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Driver.CrCasRenterPrivateDriverInformationEnName})";

                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "243")
                {
                    // if 231 then it is Bank
                    var Bank = _unitOfWork.CrCasAccountBank.Find(x => x.CrCasAccountBankCode == item.CrCasSysAdministrativeProceduresTargeted && x.CrCasAccountBankLessor == lessorCode);
                    if (Bank != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Bank.CrCasAccountBankArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Bank.CrCasAccountBankEnName})";
                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "244")
                {
                    // if 244 then it is SalesPoint
                    var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.Find(x => x.CrCasAccountSalesPointCode.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasAccountSalesPointLessor == lessorCode);
                    if (SalesPoint != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({SalesPoint.CrCasAccountSalesPointArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({SalesPoint.CrCasAccountSalesPointEnName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "211")
                {
                    // if 211 then it is Cars
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "212")
                {
                    // if 212 then it is docCar
                    var docCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(x => x.CrCasCarDocumentsMaintenanceNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarDocumentsMaintenanceLessor == lessorCode);
                    if (docCar != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({docCar.CrCasCarDocumentsMaintenanceNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({docCar.CrCasCarDocumentsMaintenanceNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "213")
                {
                    // if 213 then it is docCar
                    var maintanceCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(x => x.CrCasCarDocumentsMaintenanceNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarDocumentsMaintenanceLessor == lessorCode);
                    if (maintanceCar != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                    }
                }
                //if (item.CrCasSysAdministrativeProceduresCode == "214")
                //{
                //    // if 214 then it is FixedCar
                //    var maintanceCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(x => x.CrCasCarDocumentsMaintenanceNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarDocumentsMaintenanceLessor == lessorCode);
                //    if (maintanceCar != null)
                //    {
                //        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                //        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                //    }
                //}
                if (item.CrCasSysAdministrativeProceduresCode == "215")
                {
                    // if 215 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "216")
                {
                    // if 216 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";
                    }
                }
            };

            return View(model);
        }

        public async Task<PartialViewResult> AdminstritiveProceduresFilterDate(string startDate, string endDate)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("205", "2205001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            DateTime sd = Convert.ToDateTime(startDate);
            DateTime ed = Convert.ToDateTime(endDate);
            var AdminstritiveProcedures = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresClassification != "30" && x.CrCasSysAdministrativeProceduresClassification != "40" &&
                                                                                                   x.CrCasSysAdministrativeProceduresLessor == userLogin.CrMasUserInformationLessor &&
                                                                                                   x.CrCasSysAdministrativeProceduresDate>=sd&&x.CrCasSysAdministrativeProceduresDate<=ed,
                                                                                                    new[] { "CrCasSysAdministrativeProceduresCodeNavigation", "CrCasSysAdministrativeProcedures", "CrCasSysAdministrativeProceduresUserInsertNavigation" })
                                                                                                   .OrderByDescending(x => x.CrCasSysAdministrativeProceduresDate).ThenByDescending(x => x.CrCasSysAdministrativeProceduresTime).ToList();
            var lessorCode = userLogin.CrMasUserInformationLessor;

            var model = _mapper.Map<List<AdminstritiveProceduresVM>>(AdminstritiveProcedures);

            foreach (var item in model)
            {
                if (item.CrCasSysAdministrativeProceduresCode == "201")
                {
                    // if 201 then it is branch
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationArShortName})";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationEnShortName})";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "202")
                {
                    // if 202 then it is Doc of branch
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationArShortName})";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationEnShortName})";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "203")
                {
                    // if 202 then it is Contract Company
                    var Contract = _unitOfWork.CrMasContractCompany.Find(x => x.CrMasContractCompanyNo == item.CrCasSysAdministrativeProceduresTargeted, new[] { "CrMasContractCompanyProceduresNavigation" });
                    if (Contract != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Contract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Contract.CrMasContractCompanyProceduresNavigation.CrMasSysProceduresEnName})";
                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "204")
                {
                    // if 204 then it is Owners
                    var owner = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (owner != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({owner.CrCasOwnersArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({owner.CrCasOwnersEnName})";
                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "205")
                {
                    // if 204 then it is Benficity
                    var Benficity = _unitOfWork.CrCasBeneficiary.Find(x => x.CrCasBeneficiaryCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Benficity != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Benficity.CrCasBeneficiaryArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Benficity.CrCasBeneficiaryEnName})";
                    }

                }

                if (item.CrCasSysAdministrativeProceduresCode == "231")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (user != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";

                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "232")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (user != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";

                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "233")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (user != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";

                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "234")
                {
                    // if 231 then it is Employee
                    var user = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (user != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({user.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({user.CrMasUserInformationEnName})";
                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "246")
                {
                    // if 231 then it is Driver
                    var Driver = _unitOfWork.CrCasRenterPrivateDriverInformation.Find(x => x.CrCasRenterPrivateDriverInformationId == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Driver != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Driver.CrCasRenterPrivateDriverInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Driver.CrCasRenterPrivateDriverInformationEnName})";

                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "243")
                {
                    // if 231 then it is Bank
                    var Bank = _unitOfWork.CrCasAccountBank.Find(x => x.CrCasAccountBankCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Bank != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Bank.CrCasAccountBankArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Bank.CrCasAccountBankEnName})";
                    }

                }
                if (item.CrCasSysAdministrativeProceduresCode == "244")
                {
                    // if 244 then it is SalesPoint
                    var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.Find(x => x.CrCasAccountSalesPointCode.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim());
                    if (SalesPoint != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({SalesPoint.CrCasAccountSalesPointArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({SalesPoint.CrCasAccountSalesPointEnName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "211")
                {
                    // if 211 then it is Cars
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "212")
                {
                    // if 212 then it is docCar
                    var docCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(x => x.CrCasCarDocumentsMaintenanceNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarDocumentsMaintenanceLessor == lessorCode);
                    if (docCar != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({docCar.CrCasCarDocumentsMaintenanceNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({docCar.CrCasCarDocumentsMaintenanceNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "213")
                {
                    // if 213 then it is docCar
                    var maintanceCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(x => x.CrCasCarDocumentsMaintenanceNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarDocumentsMaintenanceLessor == lessorCode);
                    if (maintanceCar != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                    }
                }
                //if (item.CrCasSysAdministrativeProceduresCode == "214")
                //{
                //    // if 214 then it is FixedCar
                //    var maintanceCar = _unitOfWork.CrCasCarDocumentsMaintenance.Find(x => x.CrCasCarDocumentsMaintenanceNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarDocumentsMaintenanceLessor == lessorCode);
                //    if (maintanceCar != null)
                //    {
                //        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                //        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({maintanceCar.CrCasCarDocumentsMaintenanceNo})";
                //    }
                //}
                if (item.CrCasSysAdministrativeProceduresCode == "215")
                {
                    // if 215 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "216")
                {
                    // if 216 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";
                    }
                }
            };

            return PartialView("_DataTableAdminstritiveProcedures", model);
        }
    }
}
