using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
using System.Numerics;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class ContractOptionsController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IContractOptions _contractOptions;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<ContractOptionsController> _localizer;

        public ContractOptionsController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper, IUserService userService, IContractOptions contractOptions,
            IUserLoginsService userLoginsService, IToastNotification toastNotification,
            IWebHostEnvironment webHostEnvironment,IStringLocalizer<ContractOptionsController> localizer) : 
            base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _contractOptions = contractOptions;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var titles = await setTitle("110", "1110002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var contracts = await _unitOfWork.CrMasSupContractOption.GetAllAsync();
            var contract = contracts.Where(x => x.CrMasSupContractOptionsStatus == "A").ToList();
            return View(contract);
        }

        [HttpGet]
        public PartialViewResult GetContractOptionsByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var ContractOptionsbyStatusAll = _unitOfWork.CrMasSupContractOption.GetAll();
                    return PartialView("_DataTableContractOptions", ContractOptionsbyStatusAll);
                }
                var ContractOptionsbyStatus = _unitOfWork.CrMasSupContractOption.FindAll(l => l.CrMasSupContractOptionsStatus == status).ToList();
                return PartialView("_DataTableContractOptions", ContractOptionsbyStatus);
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> AddContractOptions()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("110", "1110002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var ContractOptionsCode = "";
            var ContractOptions = await _unitOfWork.CrMasSupContractOption.GetAllAsync();
            if (ContractOptions.Count() != 0)
            {
                ContractOptionsCode = (BigInteger.Parse(ContractOptions.LastOrDefault().CrMasSupContractOptionsCode) + 1).ToString();
            }
            else
            {
                ContractOptionsCode = "5100000001";
            }
            ViewBag.ContractOptionsCode = ContractOptionsCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContractOptions(ContractOptionsVM ContractOptions, IFormFile? AcceptImg, IFormFile? RejectImg, IFormFile? BlockImg)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            string foldername = $"{"images\\Common"}";
            string filePathImageAccept = "";
            string filePathImageReject = "";
            string filePathImageBlock = "";


            if (ModelState.IsValid)
            {
                if (ContractOptions != null)
                {
                    var ContractOptionsVMT = _mapper.Map<CrMasSupContractOption>(ContractOptions);
                    var ContractOption = await _unitOfWork.CrMasSupContractOption.GetAllAsync();
                    var existingContractOptions = ContractOption.FirstOrDefault(x =>
                        x.CrMasSupContractOptionsEnName == ContractOptionsVMT.CrMasSupContractOptionsEnName ||
                        x.CrMasSupContractOptionsArName == ContractOptionsVMT.CrMasSupContractOptionsArName);

                    // Generate code for the second time
                    var ContractOptionCode = (BigInteger.Parse(ContractOption.LastOrDefault().CrMasSupContractOptionsCode) + 1).ToString();
                    ContractOptions.CrMasSupContractOptionsCode = ContractOptionCode;
                    ViewBag.ContractOptionCode = ContractOptionCode;

                    if (existingContractOptions != null)
                    {
                        if (existingContractOptions.CrMasSupContractOptionsArName != null &&
                            existingContractOptions.CrMasSupContractOptionsArName == ContractOptions.CrMasSupContractOptionsArName &&
                             existingContractOptions.CrMasSupContractOptionsEnName != ContractOptions.CrMasSupContractOptionsEnName)
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
                        if (existingContractOptions.CrMasSupContractOptionsEnName != null &&
                            existingContractOptions.CrMasSupContractOptionsEnName == ContractOptions.CrMasSupContractOptionsEnName &&
                             existingContractOptions.CrMasSupContractOptionsArName != ContractOptions.CrMasSupContractOptionsArName)
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
                        if (existingContractOptions.CrMasSupContractOptionsArName != null && existingContractOptions.CrMasSupContractOptionsEnName != null
                            && existingContractOptions.CrMasSupContractOptionsEnName == ContractOptions.CrMasSupContractOptionsEnName &&
                           existingContractOptions.CrMasSupContractOptionsArName == ContractOptions.CrMasSupContractOptionsArName)
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
                        return View(ContractOptions);
                    }


                    if (AcceptImg != null)
                    {
                        string fileNameImg = ContractOptions.CrMasSupContractOptionsEnName + "_Accept_" + ContractOptions.CrMasSupContractOptionsCode.ToString().Substring(ContractOptions.CrMasSupContractOptionsCode.ToString().Length - 3);
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }
                    if (RejectImg != null)
                    {
                        string fileNameImg = ContractOptions.CrMasSupContractOptionsEnName + "_Reject_" + ContractOptions.CrMasSupContractOptionsCode.ToString().Substring(ContractOptions.CrMasSupContractOptionsCode.ToString().Length - 3);
                        filePathImageReject = await RejectImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }
                    if (BlockImg != null)
                    {
                        string fileNameImg = ContractOptions.CrMasSupContractOptionsEnName + "_Block_" + ContractOptions.CrMasSupContractOptionsCode.ToString().Substring(ContractOptions.CrMasSupContractOptionsCode.ToString().Length - 3);
                        filePathImageBlock = await BlockImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                    }

                    ContractOptionsVMT.CrMasSupContractOptionsRejectImage = filePathImageReject;
                    ContractOptionsVMT.CrMasSupContractOptionsAcceptImage = filePathImageAccept;
                    ContractOptionsVMT.CrMasSupContractOptionsBlockImage = filePathImageBlock;
                    ContractOptionsVMT.CrMasSupContractOptionsStatus = "A";
                    ContractOptionsVMT.CrMasSupContractOptionsGroup = "51";
                    await _unitOfWork.CrMasSupContractOption.AddAsync(ContractOptionsVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة اضافات", "Add Additional", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddContractOptions", ContractOptions);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("110", "1110002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var contract = await _unitOfWork.CrMasSupContractOption.GetByIdAsync(id);
            if (contract == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            var model = _mapper.Map<ContractOptionsVM>(contract);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ContractOptionsVM model, IFormFile? AcceptImg, IFormFile? RejectImg, IFormFile? BlockImg)
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
                        string fileNameImg = model.CrMasSupContractOptionsEnName + "_Accept_" + model.CrMasSupContractOptionsCode.ToString().Substring(model.CrMasSupContractOptionsCode.ToString().Length - 3);
                        filePathImageAccept = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractOptionsAcceptImage = filePathImageAccept;

                    }

                    if (RejectImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractOptionsEnName + "_Reject_" + model.CrMasSupContractOptionsCode.ToString().Substring(model.CrMasSupContractOptionsCode.ToString().Length - 3);
                        filePathImageReject = await RejectImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractOptionsRejectImage = filePathImageReject;

                    }

                    if (BlockImg != null)
                    {
                        string fileNameImg = model.CrMasSupContractOptionsEnName + "_Block_" + model.CrMasSupContractOptionsCode.ToString().Substring(model.CrMasSupContractOptionsCode.ToString().Length - 3);
                        filePathImageBlock = await BlockImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupContractOptionsBlockImage = filePathImageBlock;
                    }


                    var contract = _mapper.Map<CrMasSupContractOption>(model);

                    _unitOfWork.CrMasSupContractOption.Update(contract);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "ContractOptions");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Contract = await _unitOfWork.CrMasSupContractOption.GetByIdAsync(code);
            if (Contract != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف";
                    sEn = "Hold";
                    Contract.CrMasSupContractOptionsStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    sAr = "حذف";
                    sEn = "Remove";
                    Contract.CrMasSupContractOptionsStatus = Status.Deleted;
                }
                else if (status == "Reactivate")
                {
                    sAr = "استرجاع";
                    sEn = "Retrive";
                    Contract.CrMasSupContractOptionsStatus = Status.Acive;
                }

                await _unitOfWork.CompleteAsync();

                // SaveTracing

                var (mainTask, subTask, system, currentUser) = await SetTrace("110", "1110002", "1");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                return RedirectToAction("Index", "ContractOptions");
            }


            return View(Contract);

        }

    }
}
