using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class ContractAdditionalController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IContractAdditional _contractAdditional;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ContractAdditionalController> _localizer;


        public ContractAdditionalController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, 
            IMapper mapper, IUserService userService, IContractAdditional contractAdditional,
            IUserLoginsService userLoginsService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment , IStringLocalizer<ContractAdditionalController> localizer) : base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _contractAdditional = contractAdditional;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("110", "1110001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupContractAdditional.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupContractAdditionalStatus == "A").ToList();
            return View(contract);
        }

        [HttpGet]
        public PartialViewResult GetContractAdditionalByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var ContractAdditionalbyStatusAll = _unitOfWork.CrMasSupContractAdditional.GetAll();
                    return PartialView("_DataTableContractAdditional", ContractAdditionalbyStatusAll);
                }
                var ContractAdditionalbyStatus = _unitOfWork.CrMasSupContractAdditional.FindAll(l => l.CrMasSupContractAdditionalStatus == status).ToList();
                return PartialView("_DataTableContractAdditional", ContractAdditionalbyStatus);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> AddContractAdditional()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("110", "1110001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var ContractAdditionalCode = "";
            var ContractAdditionals = await _unitOfWork.CrMasSupContractAdditional.GetAllAsync();
            if (ContractAdditionals.Count() != 0)
            {
                 ContractAdditionalCode = (BigInteger.Parse(ContractAdditionals.LastOrDefault().CrMasSupContractAdditionalCode) + 1).ToString();
            }
            else
            {
                ContractAdditionalCode = "5000000001";
            }
            ViewBag.ContractAdditionalCode = ContractAdditionalCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContractAdditional(ContractAdditionalVM ContractAdditional,  IFormFile? AcceptImg , IFormFile? RejectImg, IFormFile? BlockImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";
            string filePathImageReject = "";
            string filePathImageBlock = "";


            if (ModelState.IsValid)
            {
                if (ContractAdditional != null)
                {
                    var ContractAdditionalVMT = _mapper.Map<CrMasSupContractAdditional>(ContractAdditional);
                    var ContractAdditionals = await _unitOfWork.CrMasSupContractAdditional.GetAllAsync();
                    var existingContractAdditional = ContractAdditionals.FirstOrDefault(x =>
                        x.CrMasSupContractAdditionalEnName == ContractAdditionalVMT.CrMasSupContractAdditionalEnName ||
                        x.CrMasSupContractAdditionalArName == ContractAdditionalVMT.CrMasSupContractAdditionalArName);

                    // Generate code for the second time
                    var ContractAdditionalCode = (BigInteger.Parse(ContractAdditionals.LastOrDefault().CrMasSupContractAdditionalCode) + 1).ToString();
                    ContractAdditional.CrMasSupContractAdditionalCode = ContractAdditionalCode;
                    ViewBag.ContractAdditionalCode = ContractAdditionalCode;

                    if (existingContractAdditional != null)
                    {
                        if (existingContractAdditional.CrMasSupContractAdditionalArName != null &&
                            existingContractAdditional.CrMasSupContractAdditionalArName == ContractAdditional.CrMasSupContractAdditionalArName &&
                             existingContractAdditional.CrMasSupContractAdditionalEnName != ContractAdditional.CrMasSupContractAdditionalEnName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                            }

                        }
                        if (existingContractAdditional.CrMasSupContractAdditionalEnName != null &&
                            existingContractAdditional.CrMasSupContractAdditionalEnName == ContractAdditional.CrMasSupContractAdditionalEnName &&
                             existingContractAdditional.CrMasSupContractAdditionalArName != ContractAdditional.CrMasSupContractAdditionalArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                            }
                            else
                            {
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }
                        if (existingContractAdditional.CrMasSupContractAdditionalArName != null && existingContractAdditional.CrMasSupContractAdditionalEnName != null
                            && existingContractAdditional.CrMasSupContractAdditionalEnName == ContractAdditional.CrMasSupContractAdditionalEnName &&
                           existingContractAdditional.CrMasSupContractAdditionalArName == ContractAdditional.CrMasSupContractAdditionalArName)
                        {
                            if (currentCulture != "en-US")
                            {
                                ModelState.AddModelError("ExistEn", "هذا الحقل مسجل من قبل.");
                                ModelState.AddModelError("ExistAr", "هذا الحقل مسجل من قبل.");

                            }
                            else
                            {
                                ModelState.AddModelError("ExistAr", "This field is Existed.");
                                ModelState.AddModelError("ExistEn", "This field is Existed.");
                            }
                        }
                        return View(ContractAdditional);
                    }


                    if (AcceptImg != null)
                    {
                        string fileNameImg = ContractAdditional.CrMasSupContractAdditionalEnName + "_Accept_" + ContractAdditional.CrMasSupContractAdditionalCode.ToString().Substring(ContractAdditional.CrMasSupContractAdditionalCode.ToString().Length - 3);
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }
                    if (RejectImg != null)
                    {
                        string fileNameImg = ContractAdditional.CrMasSupContractAdditionalEnName + "_Reject_" + ContractAdditional.CrMasSupContractAdditionalCode.ToString().Substring(ContractAdditional.CrMasSupContractAdditionalCode.ToString().Length - 3);
                        filePathImageReject = await RejectImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }
                    if (BlockImg != null)
                    {
                        string fileNameImg = ContractAdditional.CrMasSupContractAdditionalEnName + "_Block_" + ContractAdditional.CrMasSupContractAdditionalCode.ToString().Substring(ContractAdditional.CrMasSupContractAdditionalCode.ToString().Length - 3);
                        filePathImageBlock = await BlockImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }

                    ContractAdditionalVMT.CrMasSupContractAdditionalRejectImage = filePathImageReject;
                    ContractAdditionalVMT.CrMasSupContractAdditionalAcceptImage = filePathImageAccept;
                    ContractAdditionalVMT.CrMasSupContractAdditionalBlockImage= filePathImageBlock;
                    ContractAdditionalVMT.CrMasSupContractAdditionalStatus = "A";
                    ContractAdditionalVMT.CrMasSupContractAdditionalGroup = "50";
                    await _unitOfWork.CrMasSupContractAdditional.AddAsync(ContractAdditionalVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110001", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة اضافات", "Add Additional", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddContractAdditional", ContractAdditional);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("110", "1110001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupContractAdditional.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var model = _mapper.Map<ContractAdditionalVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ContractAdditionalVM model, IFormFile? AcceptImg, IFormFile? RejectImg, IFormFile? BlockImg)
        {
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";
            string filePathImageReject = "";
            string filePathImageBlock = "";
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);

          
            if (user != null)
            {
                if (model != null)
                {

                    if (AcceptImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractAdditionalEnName + "_Accept_" + model.CrMasSupContractAdditionalCode.ToString().Substring(model.CrMasSupContractAdditionalCode.ToString().Length - 3);
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractAdditionalAcceptImage = filePathImageAccept;
                    }

                    if (RejectImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractAdditionalEnName + "_Reject_" + model.CrMasSupContractAdditionalCode.ToString().Substring(model.CrMasSupContractAdditionalCode.ToString().Length - 3);
                        filePathImageReject = await RejectImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractAdditionalRejectImage = filePathImageReject;

                    }

                    if (BlockImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractAdditionalEnName + "_Block_" + model.CrMasSupContractAdditionalCode.ToString().Substring(model.CrMasSupContractAdditionalCode.ToString().Length - 3);
                        filePathImageBlock = await BlockImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractAdditionalBlockImage = filePathImageBlock;
                    }


                    var contract = _mapper.Map<CrMasSupContractAdditional>(model);

                    _unitOfWork.CrMasSupContractAdditional.Update(contract);
                     _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110001", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "ContractAdditional");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupContractAdditional.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupContractAdditionalStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    sAr = "حذف";
                    sEn = "Remove";
                    Contract.CrMasSupContractAdditionalStatus = Status.Deleted;
                }
                else if (status == "Reactivate")
                {
                    sAr = "استرجاع";
                    sEn = "Retrive";
                    Contract.CrMasSupContractAdditionalStatus = Status.Acive;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110001", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "ContractAdditional");
            }


            return View(Contract);

        }


    }
}
