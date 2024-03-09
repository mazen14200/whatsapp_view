using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.Identitiy;
using Bnan.Ui.ViewModels.MAS.UserValiditySystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Extensions.Localization;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class UsersController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserMainValidtion _userMainValidtion;
        private readonly IUserSubValidition _userSubValidition;
        private readonly IUserProcedureValidition _userProcedureValidition;
        private readonly IStringLocalizer<UsersController> _localizer;


        public UsersController(IUserService userService,
                               IAuthService authService,
                               IWebHostEnvironment webHostEnvironment,
                               UserManager<CrMasUserInformation> userManager,
                               IUnitOfWork unitOfWork, IUserLoginsService userLoginsService,
                               IMapper mapper, IUserMainValidtion userMainValidtion,
                               IUserSubValidition userSubValidition,
                               IStringLocalizer<UsersController> localizer,
                               IUserProcedureValidition userProcedureValidition) : base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _authService = authService;
            _webHostEnvironment = webHostEnvironment;
            _userLoginsService = userLoginsService;
            _userMainValidtion = userMainValidtion;
            _userSubValidition = userSubValidition;
            _userProcedureValidition = userProcedureValidition;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105001", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "0";

            // Set Title
            var titles = await setTitle("105", "1105001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);

            if (userLessor == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Exclude the current user from the list
            var usersByLessor = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);

            return View(usersByLessor.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode).ToList());
        }
        [HttpGet]
        public async Task<PartialViewResult> GetUsersByStatusAsync(string status)
        {
            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);
            if (userLessor != null)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    if (status == "all")
                    {
                        var UsersbyStatusAll = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);
                        return PartialView("_DataTableUsers", UsersbyStatusAll.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode));
                    }
                    var UsersbyStatus = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);
                    return PartialView("_DataTableUsers", UsersbyStatus.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode && x.CrMasUserInformationStatus == status));
                }
            }

            return PartialView();
        }

        public async Task<IActionResult> AddUser()
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "0";

            //To Set Title;
            var titles = await setTitle("105", "1105001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList; // Pass the callingKeys to the view


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(RegisterViewModel model, IFormFile UserSignatureFile, IFormFile UserImgFile)
        {
            var user = await _userService.GetUserByUserNameAsync(model.CrMasUserInformationCode);

            string foldername = $"{"images\\Bnan\\Users"}\\{model.CrMasUserInformationCode}";
            string filePathImage;
            string filePathSignture;

            if (UserImgFile != null)
            {
                string fileNameImg = "Image_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقت
                //string fileNameImgExtenstion = Path.GetExtension(UserImgFile.FileName);
                filePathImage = await UserImgFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
            }
            else if (UserImgFile == null && string.IsNullOrEmpty(user.CrMasUserInformationPicture))
            {
                filePathImage = "~/images/common/user.jpg";
            }
            else
            {
                filePathImage = user.CrMasUserInformationPicture;

            }
            if (UserSignatureFile != null)
            {
                string fileNameSignture = "Signture_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقت
                //string fileNameSigntureExtenstion = Path.GetExtension(UserSignatureFile.FileName);
                filePathSignture = await UserSignatureFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameSignture, ".png");
            }
            else if (UserSignatureFile == null && string.IsNullOrEmpty(user.CrMasUserInformationSignature))
            {
                filePathSignture = "~/images/common/DefualtUserSignature.png";
            }
            else
            {
                filePathSignture = user.CrMasUserInformationSignature;
            }



            if (user != null)
            {
                ModelState.AddModelError("Exist", "User Code Is Exists");
                return View(model);
            }

            model.CrMasUserInformationSignature = filePathSignture;
            model.CrMasUserInformationPicture = filePathImage;

            var crMasUserInformation = _mapper.Map<CrMasUserInformation>(model);
            var createUser = await _authService.RegisterAsync(crMasUserInformation);

            if (!createUser)
            {
                ModelState.AddModelError("Exist", "Something went wrong");
                return View(model);
            }
            //Add Role 
            var newUser = await _userService.GetUserByUserNameAsync(model.CrMasUserInformationCode);
            await _authService.AddRoleAsync(newUser, "MAS");

            //Add Main Validitions
            if (!await _userMainValidtion.AddMainValiditionsForEachUser(newUser.CrMasUserInformationCode, "1"))
            {
                ModelState.AddModelError("Exist", "Something went wrong");
                return View(model);
            }

            //Add Sub Validitions
            if (!await _userSubValidition.AddSubValiditionsForEachUser(newUser.CrMasUserInformationCode, "1"))
            {
                ModelState.AddModelError("Exist", "Something went wrong");
                return View(model);
            }

            //Add Procedures Validitions
            if (!await _userProcedureValidition.AddProceduresValiditionsForEachUser(newUser.CrMasUserInformationCode, "1"))
            {
                ModelState.AddModelError("Exist", "Something went wrong");
                return View(model);
            }


            // SaveTracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105001", "1");
            var RecordAr = $"{model.CrMasUserInformationArName} - {model.CrMasUserInformationTasksArName}";
            var RecordEn = $"{model.CrMasUserInformationEnName} - {model.CrMasUserInformationTasksEnName}";
            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "إضافة", "Add", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return RedirectToAction("Users", "Users");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "0";

            //To Set Title 
            var titles = await setTitle("105", "1105001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var user = await _userService.GetUserByUserNameAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Users");
            }
            var crMasUserInformation = _mapper.Map<RegisterViewModel>(user);
            return View(crMasUserInformation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegisterViewModel model)
        {
            var user = await _userService.GetUserByUserNameAsync(model.CrMasUserInformationCode);
            if (user != null)
            {
                // Check if the tasks have changed
                if (user.CrMasUserInformationTasksArName != model.CrMasUserInformationTasksArName
                    || user.CrMasUserInformationTasksEnName != model.CrMasUserInformationTasksEnName)
                {
                    user.CrMasUserInformationTasksArName = model.CrMasUserInformationTasksArName;
                    user.CrMasUserInformationTasksEnName = model.CrMasUserInformationTasksEnName;
                    await _userService.UpdateAsync(user);

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105001", "1");
                    var RecordAr = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationArName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationTasksArName}";
                    var RecordEn = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationEnName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationTasksEnName}";
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                }
            }

            return RedirectToAction("Users", "Users");
        }
        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var user = await _userService.GetUserByUserNameAsync(code);
            if (user != null)
            {
                if (await CheckUserSubValidationProcdures("1105001", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Hold";
                        user.CrMasUserInformationStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        user.CrMasUserInformationStatus = Status.Deleted;
                    }
                    else if (status == Status.Active)
                    {
                        sAr = "استرجاع";
                        sEn = "Retrive";
                        user.CrMasUserInformationStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105001", "1");
                    var RecordAr = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationArName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationTasksArName}";
                    var RecordEn = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationEnName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationTasksEnName}";
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    return RedirectToAction("Users", "Users");
                }

            }
            return View(user);

        }


        [HttpGet]
        public async Task<IActionResult> SystemValiditions()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105002", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "1";
            // Set Title
            var titles = await setTitle("105", "1105002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);

            if (userLessor == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Exclude the current user from the list
            var usersByLessor = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);

            return View(usersByLessor.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode && x.CrMasUserInformationStatus == Status.Active).ToList());
        }


        [HttpGet]
        public async Task<ActionResult> EditSystemValiditions(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "1";

            // Set Title
            var titles = await setTitle("105", "1105002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var user = await _userService.GetUserByUserNameAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Users");
            }
            var mainValidition = _unitOfWork.CrMasUserMainValidations.FindAll(x => x.CrMasUserMainValidationUser == id);
            var subValition = _unitOfWork.CrMasUserSubValidations.FindAll(x => x.CrMasUserSubValidationUser == id);
            var procedureValidition = _unitOfWork.CrMasUserProceduresValidations.FindAll(x => x.CrMasUserProceduresValidationCode == id);
            var procedureTasks = _unitOfWork.CrMasSysProceduresTasks.GetAll();

            RegisterViewModel viewModel = new RegisterViewModel
            {
                CrMasSysMainTasks = (List<CrMasSysMainTask>)_unitOfWork.CrMasSysMainTasks.FindAll(x => x.CrMasSysMainTasksStatus == Status.Active),
                CrMasUserMainValidations = (List<CrMasUserMainValidation>)mainValidition,

                CrMasUserInformationCode = user.CrMasUserInformationCode,
                CrMasUserInformationArName = user.CrMasUserInformationArName,
                CrMasUserInformationEnName = user.CrMasUserInformationEnName,

                CrMasSysSubTasks = (List<CrMasSysSubTask>)_unitOfWork.CrMasSysSubTasks.FindAll(x => x.CrMasSysSubTasksStatus == Status.Active),
                CrMasUserSubValidations = (List<CrMasUserSubValidation>)subValition,
                CrMasSysProceduresTasks = (List<CrMasSysProceduresTask>)procedureTasks,
                ProceduresValidations = (List<CrMasUserProceduresValidation>)procedureValidition,

            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditSystemValiditions([FromBody] CheckBoxModels model)
        {

            foreach (var checkboxMain in model.CheckboxesMainTask)
            {
                var mainTask = _unitOfWork.CrMasUserMainValidations.Find(x => x.CrMasUserMainValidationUser == model.UserId && x.CrMasUserMainValidationMainTasks == checkboxMain.id);
                if (mainTask != null) mainTask.CrMasUserMainValidationAuthorization = checkboxMain.value;
            }
            foreach (var checkboxSub in model.CheckboxesSubTask)
            {
                var mainTask = _unitOfWork.CrMasUserMainValidations.Find(x => x.CrMasUserMainValidationUser == model.UserId && x.CrMasUserMainValidationMainTasks == checkboxSub.mainTaskId);
                var subTask = _unitOfWork.CrMasUserSubValidations.Find(x => x.CrMasUserSubValidationUser == model.UserId && x.CrMasUserSubValidationMain == checkboxSub.mainTaskId && x.CrMasUserSubValidationSubTasks == checkboxSub.subTaskId);
                if (mainTask.CrMasUserMainValidationAuthorization == true)
                {
                    if (subTask != null)
                    {
                        if (subTask.CrMasUserSubValidationAuthorization == false && checkboxSub.value == true)
                        {
                            // SaveTracing
                            var (mainTask1, subTask1, system, currentUser) = await SetTrace("105", "1105002", "1");
                            var RecordAr = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.UserId).CrMasUserInformationArName} - {_unitOfWork.CrMasSysSubTask.Find(x => x.CrMasSysSubTasksCode == subTask.CrMasUserSubValidationSubTasks).CrMasSysSubTasksArName.ToString()}";
                            var RecordEn = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.UserId).CrMasUserInformationEnName} - {_unitOfWork.CrMasSysSubTask.Find(x => x.CrMasSysSubTasksCode == subTask.CrMasUserSubValidationSubTasks).CrMasSysSubTasksEnName.ToString()}";
                            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "إضافة دور", "Add role", mainTask1.CrMasSysMainTasksCode,
                            subTask1.CrMasSysSubTasksCode, mainTask1.CrMasSysMainTasksArName, subTask1.CrMasSysSubTasksArName, mainTask1.CrMasSysMainTasksEnName,
                            subTask1.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                        }

                        subTask.CrMasUserSubValidationAuthorization = checkboxSub.value;

                    }
                }
                else
                {
                    if (subTask.CrMasUserSubValidationAuthorization == true)
                    {
                        // SaveTracing
                        var (mainTask1, subTask1, system, currentUser) = await SetTrace("105", "1105002", "1");
                        var RecordAr = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.UserId).CrMasUserInformationArName} - {_unitOfWork.CrMasSysSubTask.Find(x => x.CrMasSysSubTasksCode == subTask.CrMasUserSubValidationSubTasks).CrMasSysSubTasksArName.ToString()}";
                        var RecordEn = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.UserId).CrMasUserInformationEnName} - {_unitOfWork.CrMasSysSubTask.Find(x => x.CrMasSysSubTasksCode == subTask.CrMasUserSubValidationSubTasks).CrMasSysSubTasksEnName.ToString()}";
                        await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "حذف دور", "Remove role", mainTask1.CrMasSysMainTasksCode,
                        subTask1.CrMasSysSubTasksCode, mainTask1.CrMasSysMainTasksArName, subTask1.CrMasSysSubTasksArName, mainTask1.CrMasSysMainTasksEnName,
                        subTask1.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    }

                    subTask.CrMasUserSubValidationAuthorization = false;
                }

            }
            foreach (var checkboxProcedure in model.CheckboxesProcedureTask)
            {
                var mainTask = _unitOfWork.CrMasUserMainValidations.Find(x => x.CrMasUserMainValidationUser == model.UserId && x.CrMasUserMainValidationMainTasks == checkboxProcedure.mainTaskId);
                var subTask = _unitOfWork.CrMasUserSubValidations.Find(x => x.CrMasUserSubValidationUser == model.UserId && x.CrMasUserSubValidationMain == checkboxProcedure.mainTaskId && x.CrMasUserSubValidationSubTasks == checkboxProcedure.subTaskId);
                var procedureTask = _unitOfWork.CrMasUserProceduresValidations.Find(x => x.CrMasUserProceduresValidationCode == model.UserId && x.CrMasUserProceduresValidationMainTask == checkboxProcedure.mainTaskId && x.CrMasUserProceduresValidationSubTasks == checkboxProcedure.subTaskId);

                if (mainTask.CrMasUserMainValidationAuthorization == true && subTask.CrMasUserSubValidationAuthorization == true)
                {
                    if (procedureTask != null && checkboxProcedure != null)
                    {
                        if (checkboxProcedure.procedureName.ToLower() == "insert") procedureTask.CrMasUserProceduresValidationInsertAuthorization = checkboxProcedure.value;
                        if (checkboxProcedure.procedureName.ToLower() == "update") procedureTask.CrMasUserProceduresValidationUpDateAuthorization = checkboxProcedure.value;
                        if (checkboxProcedure.procedureName.ToLower() == "hold") procedureTask.CrMasUserProceduresValidationHoldAuthorization = checkboxProcedure.value;
                        if (checkboxProcedure.procedureName.ToLower() == "unhold") procedureTask.CrMasUserProceduresValidationUnHoldAuthorization = checkboxProcedure.value;
                        if (checkboxProcedure.procedureName.ToLower() == "delete") procedureTask.CrMasUserProceduresValidationDeleteAuthorization = checkboxProcedure.value;
                        if (checkboxProcedure.procedureName.ToLower() == "undelete") procedureTask.CrMasUserProceduresValidationUnDeleteAuthorization = checkboxProcedure.value;


                    }
                }
                else
                {
                    procedureTask.CrMasUserProceduresValidationInsertAuthorization = false;
                    procedureTask.CrMasUserProceduresValidationUpDateAuthorization = false;
                    procedureTask.CrMasUserProceduresValidationHoldAuthorization = false;
                    procedureTask.CrMasUserProceduresValidationUnHoldAuthorization = false;
                    procedureTask.CrMasUserProceduresValidationDeleteAuthorization = false;
                    procedureTask.CrMasUserProceduresValidationUnDeleteAuthorization = false;
                }
            }
            _unitOfWork.Complete();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> MyAccount()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105003", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            // Set Title
            var titles = await setTitle("105", "1105001", "1");
            await ViewData.SetPageTitleAsync(titles[0], "", titles[2], "تعديل", "Edit", titles[3]);
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList; // Pass the callingKeys to the view
            var crMasUserInformation = _mapper.Map<RegisterViewModel>(user);
            if (user == null) return RedirectToAction("Login", "Account");
            return View(crMasUserInformation);
        }

        [HttpPost]
        public async Task<IActionResult> MyAccount(RegisterViewModel model, IFormFile UserSignatureFile, IFormFile UserImgFile, string countryCode)
        {
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Account");

            string foldername = $"{"images\\Bnan\\Users"}\\{model.CrMasUserInformationCode}";
            string filePathImage;
            string filePathSignture;
            if (UserImgFile != null)
            {
                string fileNameImg = "Image_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقت
                filePathImage = await UserImgFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png", user.CrMasUserInformationPicture);
            }
            else if (UserImgFile == null && string.IsNullOrEmpty(user.CrMasUserInformationPicture))
            {
                filePathImage = "~/images/common/user.jpg";
            }
            else
            {
                filePathImage = user.CrMasUserInformationPicture;

            }
            
            if (UserSignatureFile != null)
            {
                string fileNameSignture = "Signture_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقتs
                filePathSignture = await UserSignatureFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameSignture, ".png",user.CrMasUserInformationSignature);
            }
            else if (UserSignatureFile == null && string.IsNullOrEmpty(user.CrMasUserInformationSignature))
            {
                filePathSignture = "~/images/common/DefualtUserSignature.png";
            }
            else
            {
                filePathSignture = user.CrMasUserInformationSignature;
            }
            user.CrMasUserInformationCallingKey = model.CrMasUserInformationCallingKey;
            user.CrMasUserInformationMobileNo = model.CrMasUserInformationMobileNo;
            user.CrMasUserInformationEmail = model.CrMasUserInformationEmail;
            user.CrMasUserInformationExitTimer = model.CrMasUserInformationExitTimer;
            user.CrMasUserInformationSignature = filePathSignture;
            user.CrMasUserInformationPicture = filePathImage;
            await _userManager.UpdateAsync(user);

            // SaveTracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105003", "1");
            var RecordAr = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationArName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationTasksArName}";
            var RecordEn = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationEnName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == model.CrMasUserInformationCode).CrMasUserInformationTasksEnName}";
            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            if (model != null)
            {
                var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
                if (user == null) return RedirectToAction("Login", "Account");

                // Check current password 
                if (user.CrMasUserInformationPassWord.Trim() != model.CurrentPassword.Trim())
                {
                    if (currentCulture == "en-US") ModelState.AddModelError("NotExist", "Current password is incorrect. Please try again.");
                    else ModelState.AddModelError("NotExist", "كلمة السر الحالية غير صحيحة, يرجي اعادة المحاولة");
                    return View(model);
                }
                if (model.NewPassword != model.ConfirmPassword)
                {
                    if (currentCulture == "en-US") ModelState.AddModelError("Exist", "New password does not match. Enter new password again here.");
                    else ModelState.AddModelError("Exist", "كلمة السر وتأكيد كلمة السر غير مطابقان, يرجي اعادة المحاولة");

                    return View(model);
                }
                var result = await _userManager.ChangePasswordAsync(user, user.CrMasUserInformationPassWord.Trim(), model.NewPassword);
                if (result.Succeeded)
                {
                    user.CrMasUserInformationPassWord = model.NewPassword;
                    user.CrMasUserInformationChangePassWordLastDate = DateTime.Now.Date;
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("105", "1105003", "1");
                    var RecordAr = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationArName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationTasksArName}";
                    var RecordEn = $"{_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationEnName} - {_unitOfWork.CrMasUserInformation.Find(x => x.CrMasUserInformationCode == user.CrMasUserInformationCode).CrMasUserInformationTasksEnName}";
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تغيير الباسورد", "password Changed", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                }
            }
            else
            {
                ModelState.AddModelError("Exist", "Something wrong happed , please try again");
                return View(model);
            }



            return RedirectToAction("Index", "Home");
        }

    }
}