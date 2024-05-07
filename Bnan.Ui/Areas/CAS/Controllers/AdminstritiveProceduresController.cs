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
        private IUserLoginsService _userLoginsService { get; set; }
        public AdminstritiveProceduresController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper ,IUserLoginsService userLoginsService) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
        }

        public async Task<IActionResult> AdminstritiveProcedures()
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "0";

            var userLogin = await _userManager.GetUserAsync(User);
            var titles = await setTitle("205", "2205001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);



            var StartDate_Table = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresClassification != "30" &&
            x.CrCasSysAdministrativeProceduresClassification != "40")?.Max(x => x.CrCasSysAdministrativeProceduresDate);

            var today = DateTime.Today;
            if (StartDate_Table != null)
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
            var AdminstritiveProcedures = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresClassification != "30" && x.CrCasSysAdministrativeProceduresClassification != "40" &&
                                                                                                   x.CrCasSysAdministrativeProceduresLessor == userLogin.CrMasUserInformationLessor&&
                                                                                                   x.CrCasSysAdministrativeProceduresDate >= sd && x.CrCasSysAdministrativeProceduresDate <= ed,
                                                                                                    new[] { "CrCasSysAdministrativeProceduresCodeNavigation", "CrCasSysAdministrativeProcedures", "CrCasSysAdministrativeProceduresUserInsertNavigation" })
                                                                                                   .OrderByDescending(x => x.CrCasSysAdministrativeProceduresDate).ThenByDescending(x => x.CrCasSysAdministrativeProceduresTime).ToList();
            var model = _mapper.Map<List<AdminstritiveProceduresVM>>(AdminstritiveProcedures);

            // SaveTracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("205", "2205001", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            foreach (var item in model)
            {
                if (item.CrCasSysAdministrativeProceduresCode == "100" || item.CrCasSysAdministrativeProceduresCode == "101" || 
                    item.CrCasSysAdministrativeProceduresCode == "102" ||
                    item.CrCasSysAdministrativeProceduresCode == "103" || item.CrCasSysAdministrativeProceduresCode == "104")
                {
                    var Branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Branch != null)
                    {
                        // if 201 then it is branch
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Branch.CrCasBranchInformationArShortName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Branch.CrCasBranchInformationEnShortName})";

                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "201")
                {
                    var Branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Branch != null)
                    {
                        // if 201 then it is branch
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Branch.CrCasBranchInformationArShortName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Branch.CrCasBranchInformationEnShortName})";

                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "202")
                {
                    // if 202 then it is Doc of branch
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationArShortName})";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({item.CrCasSysAdministrativeProcedures.CrCasBranchInformationEnShortName})";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "203")
                {
                    // if 203 then it is Contract Company
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
                if (item.CrCasSysAdministrativeProceduresCode == "247")
                {
                    // if 247 then it is Renter
                    var Renter = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == item.CrCasSysAdministrativeProceduresTargeted );
                    if (Renter != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Renter.CrMasRenterInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Renter.CrMasRenterInformationEnName})";

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
                if (item.CrCasSysAdministrativeProceduresCode == "214")
                {
                    // if 214 then it is FixedCar
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "215")
                {
                    // if 215 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    var branch1 = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode.Trim() == item.CrCasSysAdministrativeProceduresCarFrom.Trim());
                    var branch2 = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode.Trim() == item.CrCasSysAdministrativeProceduresCarTo.Trim());
                    if (car != null && branch1 != null && branch2 != null)
                    {
                        //item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        //item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";

                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo}) من ({branch1.CrCasBranchInformationArShortName}) إلى ({branch2.CrCasBranchInformationArShortName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo}) From ({branch1.CrCasBranchInformationEnShortName}) To ({branch2.CrCasBranchInformationEnShortName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "216")
                {
                    // if 216 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    var owner1 = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode.Trim() == item.CrCasSysAdministrativeProceduresCarFrom.Trim());
                    var owner2 = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode.Trim() == item.CrCasSysAdministrativeProceduresCarTo.Trim());
                    string[]? owner1_Ar = owner1?.CrCasOwnersArName?.Split(null);
                    string[]? owner1_En = owner1?.CrCasOwnersEnName?.Split(null);
                    string[]? owner2_Ar = owner2?.CrCasOwnersArName?.Split(null);
                    string[]? owner2_En = owner2?.CrCasOwnersEnName?.Split(null);

                    if (car != null )
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                        //if (owner1_Ar?.Count() >1 && owner1_En?.Count() > 1 && owner2_Ar?.Count() > 1 && owner2_En?.Count() > 1)
                        //{
                        //    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} من {owner1_Ar[0] +" "+ owner1_Ar[1]} إلى {owner2_Ar[0] + " "+ owner2_Ar[1]} ({car.CrCasCarInformationConcatenateArName})";
                        //    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} From {owner1_En[0] + " " + owner1_En[1]} To {owner2_En[0] + " "+ owner2_En[1]} ({car.CrCasCarInformationConcatenateEnName})";
                        //}
                        //else
                        //{
                        //    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        //    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";
                        //}
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "217")
                {
                    // if 217 then it is Offer Car 
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "218")
                {
                    // if 218 then it is Confirm Offer Car 
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "219")
                {
                    // if 219 then it is Car Price
                    var carDistribution = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicDistributionCode.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasPriceCarBasicLessorCode == lessorCode, new[] { "CrCasPriceCarBasicDistributionCodeNavigation" });
                    if (carDistribution != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({carDistribution.CrCasPriceCarBasicDistributionCodeNavigation.CrMasSupCarDistributionCode})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({carDistribution.CrCasPriceCarBasicDistributionCodeNavigation.CrMasSupCarDistributionConcatenateEnName})";
                    }
                }
                if(item.CrCasSysAdministrativeProceduresCode == "303")
                {
                    // if 219 then it is Car Price
                    var userINfo = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrMasUserInformationLessor == lessorCode, new[] { "CrCasPriceCarBasicDistributionCodeNavigation" });
                    if (userINfo != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({userINfo.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({userINfo.CrMasUserInformationEnName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "242")
                {
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName}";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName}";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "241")
                {
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName}";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName}";
                }
            };

            return View(model);
        }

        public async Task<PartialViewResult> AdminstritiveProceduresFilterDate(string startDate, string endDate)
        {
            //sidebar Active
            ViewBag.id = "#sidebarReport";
            ViewBag.no = "0";

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
                if (item.CrCasSysAdministrativeProceduresCode == "100" || item.CrCasSysAdministrativeProceduresCode == "101" ||
                item.CrCasSysAdministrativeProceduresCode == "102" ||
                item.CrCasSysAdministrativeProceduresCode == "103" || item.CrCasSysAdministrativeProceduresCode == "104")
                {
                    var Branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Branch != null)
                    {
                        // if 201 then it is branch
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Branch.CrCasBranchInformationArShortName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Branch.CrCasBranchInformationEnShortName})";

                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "201")
                {
                    var Branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == item.CrCasSysAdministrativeProceduresTargeted);
                    if(Branch != null)
                    {
                        // if 201 then it is branch
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Branch.CrCasBranchInformationArShortName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Branch.CrCasBranchInformationEnShortName})";

                    }
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
                if (item.CrCasSysAdministrativeProceduresCode == "214")
                {
                    // if 214 then it is FixedCar
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "215")
                {
                    // if 215 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    var branch1 = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode.Trim() == item.CrCasSysAdministrativeProceduresCarFrom.Trim());
                    var branch2 = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode.Trim() == item.CrCasSysAdministrativeProceduresCarTo.Trim());
                    if (car != null && branch1 != null && branch2 != null)
                    {
                        //item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        //item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";

                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo}) من ({branch1.CrCasBranchInformationArShortName}) إلى ({branch2.CrCasBranchInformationArShortName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo}) From ({branch1.CrCasBranchInformationEnShortName}) To ({branch2.CrCasBranchInformationEnShortName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "216")
                {
                    // if 216 then it is Car From branch To another
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    var owner1 = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode.Trim() == item.CrCasSysAdministrativeProceduresCarFrom.Trim());
                    var owner2 = _unitOfWork.CrCasOwner.Find(x => x.CrCasOwnersCode.Trim() == item.CrCasSysAdministrativeProceduresCarTo.Trim());
                    string[]? owner1_Ar = owner1?.CrCasOwnersArName?.Split(null);
                    string[]? owner1_En = owner1?.CrCasOwnersEnName?.Split(null);
                    string[]? owner2_Ar = owner2?.CrCasOwnersArName?.Split(null);
                    string[]? owner2_En = owner2?.CrCasOwnersEnName?.Split(null);

                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                        //if (owner1_Ar?.Count() >1 && owner1_En?.Count() > 1 && owner2_Ar?.Count() > 1 && owner2_En?.Count() > 1)
                        //{
                        //    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} من {owner1_Ar[0] +" "+ owner1_Ar[1]} إلى {owner2_Ar[0] + " "+ owner2_Ar[1]} ({car.CrCasCarInformationConcatenateArName})";
                        //    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} From {owner1_En[0] + " " + owner1_En[1]} To {owner2_En[0] + " "+ owner2_En[1]} ({car.CrCasCarInformationConcatenateEnName})";
                        //}
                        //else
                        //{
                        //    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationConcatenateArName})";
                        //    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationConcatenateEnName})";
                        //}
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "217")
                {
                    // if 217 then it is Offer Car 
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "218")
                {
                    // if 218 then it is Confirm Offer Car 
                    var car = _unitOfWork.CrCasCarInformation.Find(x => x.CrCasCarInformationSerailNo.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasCarInformationLessor == lessorCode);
                    if (car != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({car.CrCasCarInformationSerailNo})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({car.CrCasCarInformationSerailNo})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "219")
                {
                    // if 219 then it is Car Price
                    var carDistribution = _unitOfWork.CrCasPriceCarBasic.Find(x => x.CrCasPriceCarBasicDistributionCode.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrCasPriceCarBasicLessorCode == lessorCode, new[] { "CrCasPriceCarBasicDistributionCodeNavigation" });
                    if (carDistribution != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({carDistribution.CrCasPriceCarBasicDistributionCodeNavigation.CrMasSupCarDistributionCode})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({carDistribution.CrCasPriceCarBasicDistributionCodeNavigation.CrMasSupCarDistributionConcatenateEnName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "303")
                {
                    var userINfo = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode.Trim() == item.CrCasSysAdministrativeProceduresTargeted.Trim() && x.CrMasUserInformationLessor == lessorCode, new[] { "CrCasPriceCarBasicDistributionCodeNavigation" });
                    if (userINfo != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({userINfo.CrMasUserInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({userINfo.CrMasUserInformationEnName})";
                    }
                }
                if (item.CrCasSysAdministrativeProceduresCode == "242")
                {
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName}";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName}";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "241")
                {
                    item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName}";
                    item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName}";
                }
                if (item.CrCasSysAdministrativeProceduresCode == "247")
                {
                    // if 247 then it is Renter
                    var Renter = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == item.CrCasSysAdministrativeProceduresTargeted);
                    if (Renter != null)
                    {
                        item.NameOfTargetAr = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresArName} ({Renter.CrMasRenterInformationArName})";
                        item.NameOfTargetEn = $"{item.CrCasSysAdministrativeProceduresCodeNavigation.CrMasSysProceduresEnName} ({Renter.CrMasRenterInformationEnName})";

                    }

                }
            };

            return PartialView("_DataTableAdminstritiveProcedures", model);
        }
    }
}
