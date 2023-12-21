﻿using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ContractController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ContractController> _localizer;
        private readonly IContract _ContractServices;
        public ContractController(IStringLocalizer<ContractController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IContract contractServices) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _ContractServices = contractServices;
        }
        public async Task<IActionResult> CreateContract()
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var userInformation = _unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationLessor == lessorCode && x.CrMasUserInformationCode == userLogin.CrMasUserInformationCode, new[] { "CrMasUserBranchValidities.CrMasUserBranchValidity1" });
            var branchesValidite = userInformation.CrMasUserBranchValidities.Where(x => x.CrMasUserBranchValidityBranchStatus == Status.Active);
            List<CrCasBranchInformation> branches = new List<CrCasBranchInformation>();
            if (branchesValidite != null)
            {
                foreach (var item in branchesValidite)
                {
                    branches.Add(item.CrMasUserBranchValidity1);
                }
            }
            else
            {
                return RedirectToAction("Logout", "Account", new { area = "Identity" });
            }

            var selectBranch = userLogin.CrMasUserInformationDefaultBranch;
            if (selectBranch == null || selectBranch == "000") selectBranch = "100";
            var checkBranch = branches.Find(x => x.CrCasBranchInformationCode == selectBranch);
            if (checkBranch == null) selectBranch = branches.FirstOrDefault().CrCasBranchInformationCode;
            var branch = _unitOfWork.CrCasBranchInformation.Find(x => x.CrCasBranchInformationCode == selectBranch);


            BSLayoutVM bSLayoutVM = new BSLayoutVM()
            {
                CrCasBranchInformations = branches,
                SelectedBranch = selectBranch,
                CrCasBranchInformation = branch,
            };
            return View(bSLayoutVM);
        }
        [HttpGet]
        public async Task<IActionResult> GetRenter(string RenterId)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            //First check if bnan have this renter id or not 
            var BnanRenterInfo = _unitOfWork.CrMasRenterInformation.Find(x => x.CrMasRenterInformationId == RenterId, new[] { "CrMasRenterInformationGenderNavigation",
                                                                                                                           "CrMasRenterInformationNationalityNavigation",
                                                                                                                           "CrMasRenterInformationProfessionNavigation",
                                                                                                                           "CrMasRenterInformationEmployerNavigation",
                                                                                                                           "CrMasRenterInformationDrivingLicenseTypeNavigation"});

            if (BnanRenterInfo != null)
            {
                var LessorRenterInfo = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == RenterId && x.CrCasRenterLessorId == lessorCode);
                var RenterPost = _unitOfWork.CrMasRenterPost.Find(x => x.CrMasRenterPostCode == RenterId);
                //this model for View Only 
                RenterInformationsVM renterInformationsVM = new RenterInformationsVM
                {
                    PersonalArName = BnanRenterInfo?.CrMasRenterInformationArName,
                    PersonalEnName = BnanRenterInfo?.CrMasRenterInformationEnName,
                    PersonalArGender = BnanRenterInfo?.CrMasRenterInformationGenderNavigation?.CrMasSupRenterGenderArName,
                    PersonalEnGender = BnanRenterInfo?.CrMasRenterInformationGenderNavigation?.CrMasSupRenterGenderEnName,
                    PersonalArNationality = BnanRenterInfo?.CrMasRenterInformationNationalityNavigation?.CrMasSupRenterNationalitiesArName,
                    PersonalEnNationality = BnanRenterInfo?.CrMasRenterInformationNationalityNavigation?.CrMasSupRenterNationalitiesEnName,
                    PersonalArProfessions = BnanRenterInfo?.CrMasRenterInformationProfessionNavigation?.CrMasSupRenterProfessionsArName,
                    PersonalEnProfessions = BnanRenterInfo?.CrMasRenterInformationProfessionNavigation?.CrMasSupRenterProfessionsEnName,
                    PersonalEmail = BnanRenterInfo?.CrMasRenterInformationEmail,
                    EmployerArName = BnanRenterInfo?.CrMasRenterInformationEmployerNavigation?.CrMasSupRenterEmployerArName,
                    EmployerEnName = BnanRenterInfo?.CrMasRenterInformationEmployerNavigation?.CrMasSupRenterEmployerEnName,
                    LicenseArName = BnanRenterInfo?.CrMasRenterInformationDrivingLicenseTypeNavigation?.CrMasSupRenterDrivingLicenseArName,
                    LicenseEnName = BnanRenterInfo?.CrMasRenterInformationDrivingLicenseTypeNavigation?.CrMasSupRenterDrivingLicenseEnName,
                    LicenseExpiryDate = BnanRenterInfo?.CrMasRenterInformationExpiryDrivingLicenseDate,
                    LicenseIssuedDate = BnanRenterInfo?.CrMasRenterInformationIssueIdDate,
                    PostArNameConcenate = RenterPost?.CrMasRenterPostArConcatenate,
                    PostEnNameConcenate = RenterPost?.CrMasRenterPostEnConcatenate,
                    MobileNumber = BnanRenterInfo?.CrMasRenterInformationMobile,
                    BirthDate = BnanRenterInfo?.CrMasRenterInformationBirthDate

                };
                return Json(renterInformationsVM);
            }
            else
            {
                var elmRenterInfo = _unitOfWork.CrElmPersonal.Find(x => x.CrElmPersonalCode == RenterId);
                if (elmRenterInfo != null)
                {
                    var elmRenterLicince = _unitOfWork.CrElmLicense.Find(x => x.CrElmLicensePersonId == RenterId);
                    var elmRenterEmployeer = _unitOfWork.CrElmEmployer.Find(x => x.CrElmEmployerCode == RenterId);
                    var elmRenterPost = _unitOfWork.CrElmPost.Find(x => x.CrElmPostCode == RenterId);

                    var masRenterInformation = await _ContractServices.AddRenterFromElmToMasRenter(RenterId, elmRenterEmployeer, elmRenterInfo, elmRenterLicince);
                    var masRenterPost = await _ContractServices.AddRenterFromElmToMasRenterPost(RenterId, elmRenterPost);
                    var casRenterLessor = await _ContractServices.AddRenterFromElmToCasRenterLessor(lessorCode, masRenterInformation, masRenterPost);
                    if (masRenterInformation != null && masRenterPost != null && casRenterLessor != null)
                    {
                        //try for test
                        try
                        {
                            await _unitOfWork.CompleteAsync();
                        }
                        catch (Exception ex)
                        {
                            return Json(null);
                            throw;
                        }
                    }
                    else
                    {
                        return Json(null);
                    }

                    //this model for View Only 
                    RenterInformationsVM renterInformationsVM = new RenterInformationsVM
                    {
                        PersonalArName = elmRenterInfo?.CrElmPersonalArName,
                        PersonalEnName = elmRenterInfo?.CrElmPersonalEnName,
                        PersonalArGender = elmRenterInfo?.CrElmPersonalArGender,
                        PersonalEnGender = elmRenterInfo?.CrElmPersonalEnGender,
                        PersonalArNationality = elmRenterInfo?.CrElmPersonalArNationality,
                        PersonalEnNationality = elmRenterInfo?.CrElmPersonalEnNationality,
                        PersonalArProfessions = elmRenterInfo?.CrElmPersonalArNationality,
                        PersonalEnProfessions = elmRenterInfo?.CrElmPersonalEnProfessions,
                        PersonalEmail = elmRenterInfo?.CrElmPersonalEmail,
                        EmployerArName = elmRenterEmployeer?.CrElmEmployerArName,
                        EmployerEnName = elmRenterEmployeer?.CrElmEmployerEnName,
                        LicenseArName = elmRenterLicince.CrElmLicenseArName,
                        LicenseEnName = elmRenterLicince.CrElmLicenseEnName,
                        LicenseExpiryDate = elmRenterLicince.CrElmLicenseExpiryDate,
                        LicenseIssuedDate = elmRenterLicince.CrElmLicenseIssuedDate,
                        PostArNameConcenate = elmRenterPost.CrElmPostCityArName + "-" + elmRenterPost.CrElmPostRegionsArName + "-" + elmRenterPost.CrElmPostDistrictArName + "-" + elmRenterPost.CrElmPostStreetArName + "-" + elmRenterPost.CrElmPostUnitNo,
                        PostEnNameConcenate = elmRenterPost.CrElmPostCityEnName + "-" + elmRenterPost.CrElmPostRegionsEnName + "-" + elmRenterPost.CrElmPostDistrictEnName + "-" + elmRenterPost.CrElmPostStreetEnName + "-" + elmRenterPost.CrElmPostUnitNo,
                        MobileNumber = elmRenterInfo?.CrElmPersonalMobile,
                        BirthDate = elmRenterInfo.CrElmPersonalBirthDate
                    };
                    return Json(renterInformationsVM);
                }
            }
            return Json(null);
        }

    }
}
