using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Bnan.Ui.ViewModels.Identitiy;
using Bnan.Ui.ViewModels.MAS.UserValiditySystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class EmployeesController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserLoginsService _userLoginsService;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly IUserMainValidtion _userMainValidtion;
        private readonly IUserSubValidition _userSubValidition;
        private readonly IUserProcedureValidition _userProcedureValidition;
        private readonly IUserBranchValidity _userBranchValidity;
        private readonly IUserContractValididation _userContractValididation;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<EmployeesController> _localizer;


        public EmployeesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, IUserService userService, IWebHostEnvironment webHostEnvironment, IUserLoginsService userLoginsService, IUserMainValidtion userMainValidtion, IUserSubValidition userSubValidition, IUserProcedureValidition userProcedureValidition, IUserBranchValidity userBranchValidity, IToastNotification toastNotification, IStringLocalizer<EmployeesController> localizer, IUserContractValididation userContractValididation, IAdminstritiveProcedures adminstritiveProcedures) : base(userManager, unitOfWork, mapper)
        {
            _authService = authService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _userLoginsService = userLoginsService;
            _userMainValidtion = userMainValidtion;
            _userSubValidition = userSubValidition;
            _userProcedureValidition = userProcedureValidition;
            _userBranchValidity = userBranchValidity;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _userContractValididation = userContractValididation;
            _adminstritiveProcedures = adminstritiveProcedures;
        }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "0";

            // Set Title
            var titles = await setTitle("206", "2206001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);

            if (userLessor == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Exclude the current user from the list
            var usersByLessor = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);
            var userWithOutManger = usersByLessor.Where(x => x.CrMasUserInformationCode != ("CAS" + userLessor.CrMasUserInformationLessor));
            return View(userWithOutManger.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode&&x.CrMasUserInformationStatus==Status.Active).ToList());
        }

        [HttpGet]
        public async Task<PartialViewResult> GetEmployeesByStatusAsync(string status)
        {
            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);
            if (userLessor != null)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    var UsersbyStatusAll = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);
                    var UsersWithOutManger = UsersbyStatusAll.Where(x => x.CrMasUserInformationCode != ("CAS" + userLessor.CrMasUserInformationLessor));

                    if (status == Status.All)
                    {
                        return PartialView("_DataTableEmployees", UsersWithOutManger.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode&&x.CrMasUserInformationStatus!=Status.Deleted));
                    }
                    return PartialView("_DataTableEmployees", UsersWithOutManger.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode && x.CrMasUserInformationStatus == status));
                }
            }

            return PartialView();
        }

        public async Task<IActionResult> AddEmployee()
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "0";
            //To Set Title;
            var titles = await setTitle("206", "2206001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "اضافة", "Create", titles[3]);
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList; // Pass the callingKeys to the view
            var currentUser = await _userManager.GetUserAsync(User);
            var lastUser = _userManager.Users.ToList().LastOrDefault(x => x.CrMasUserInformationLessor == currentUser.CrMasUserInformationLessor);
            ViewBag.Branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == currentUser.CrMasUserInformationLessor && x.CrCasBranchInformationStatus == Status.Active);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(RegisterViewModel model, IFormFile? UserSignatureFile, IFormFile? UserImgFile, List<string> CheckboxBranchesWithData,
            bool CrMasUserInformationAuthorizationAdmin, bool CrMasUserInformationAuthorizationOwner, bool CrMasUserInformationAuthorizationBranch)
        {
            var callingKeys = _unitOfWork.CrMasSysCallingKeys.FindAll(x => x.CrMasSysCallingKeysStatus == Status.Active);
            var callingKeyList = callingKeys.Select(c => new SelectListItem { Value = c.CrMasSysCallingKeysCode.ToString(), Text = c.CrMasSysCallingKeysNo }).ToList();
            ViewData["CallingKeys"] = callingKeyList; // Pass the callingKeys to the view
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.Branches = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == currentUser.CrMasUserInformationLessor);

            if (ModelState.IsValid)
            {
                var userLogin = await _userManager.GetUserAsync(User);
                var user = await _userService.GetUserByUserNameAsync(model.CrMasUserInformationCode);
                if (user != null)
                {
                    
                    ModelState.AddModelError("Exist", _localizer["EmployeeExist"]);
                    return View(model);
                }

                string foldername = $"{"images\\Company"}\\{userLogin.CrMasUserInformationLessor}\\{"Users"}\\{model.CrMasUserInformationCode}";
                string filePathImage;
                string filePathSignture;

                if (UserImgFile != null)
                {
                    string fileNameImg = "Image";
                    filePathImage = await UserImgFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                }
                else
                {
                    filePathImage = "~/images/common/user.jpg";
                }
                if (UserSignatureFile != null)
                {
                    string fileNameSignture = "Signture";
                    filePathSignture = await UserSignatureFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameSignture, ".png");
                }
                else
                {
                    filePathSignture = "~/images/common/DefualtUserSignature.png";
                }


                model.CrMasUserInformationSignature = filePathSignture;
                model.CrMasUserInformationPicture = filePathImage;
                model.CrMasUserInformationLessor = userLogin.CrMasUserInformationLessor;
                model.CrMasUserInformationAuthorizationAdmin = CrMasUserInformationAuthorizationAdmin;
                model.CrMasUserInformationAuthorizationOwner = CrMasUserInformationAuthorizationOwner;
                model.CrMasUserInformationAuthorizationBranch = CrMasUserInformationAuthorizationBranch;
                var crMasUserInformation = _mapper.Map<CrMasUserInformation>(model);
                var createUser = await _authService.RegisterForCasAsync(crMasUserInformation);

                if (!createUser)
                {
                    ModelState.AddModelError("Exist", "Something went wrong");
                    return View(model);
                }
                //Add Role 
                var newUser = await _userService.GetUserByUserNameAsync(crMasUserInformation.CrMasUserInformationCode);
                await _authService.AddRoleAsync(newUser, "CAS");

                //Add Main Validitions
                if (!await _userMainValidtion.AddMainValiditionsForEachUser(newUser.CrMasUserInformationCode, "2"))
                {
                    ModelState.AddModelError("Exist", "Something went wrong");
                    return View(model);
                }

                //Add Sub Validitions
                if (!await _userSubValidition.AddSubValiditionsForEachUser(newUser.CrMasUserInformationCode, "2"))
                {
                    ModelState.AddModelError("Exist", "Something went wrong");
                    return View(model);
                }

                //Add Procedures Validitions
                if (!await _userProcedureValidition.AddProceduresValiditionsForEachUser(newUser.CrMasUserInformationCode, "2"))
                {
                    ModelState.AddModelError("Exist", "Something went wrong");
                    return View(model);
                }



                //Add Contract Validitions
                if (!await _userContractValididation.AddContractValiditionsForEachUserInCas(newUser.CrMasUserInformationCode, null))
                {
                    ModelState.AddModelError("Exist", "Something went wrong");
                    return View(model);
                }

                List<CheckboxBranchesAuthData> checkboxDataList = new List<CheckboxBranchesAuthData>();

                // Deserialize and filter the checkbox data
                foreach (var item in CheckboxBranchesWithData)
                {
                    List<CheckboxBranchesAuthData> deserializedData = JsonConvert.DeserializeObject<List<CheckboxBranchesAuthData>>(item);
                    checkboxDataList.AddRange(deserializedData);
                }

                foreach (var item in checkboxDataList)
                {
                    var id = item.Id;
                    var value = item.Value;
                    if (newUser.CrMasUserInformationAuthorizationBranch == true)
                    {
                        if (value.ToLower() == "true")
                        {
                            await _userBranchValidity.AddUserBranchValidity(newUser.CrMasUserInformationCode, userLogin.CrMasUserInformationLessor, id, Status.Active);
                        }
                        else
                        {
                            await _userBranchValidity.AddUserBranchValidity(newUser.CrMasUserInformationCode, userLogin.CrMasUserInformationLessor, id, Status.Deleted);
                        }
                    }
                    else
                    {
                        await _userBranchValidity.AddUserBranchValidity(newUser.CrMasUserInformationCode, userLogin.CrMasUserInformationLessor, id, Status.Deleted);

                    }
                }
                // SaveTracing
                var (mainTask, subTask, system, currentUserr) = await SetTrace("206", "2206001", "2");
                await _userLoginsService.SaveTracing(currentUserr.CrMasUserInformationCode, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "234", "20", currentUser.CrMasUserInformationLessor, "100",
                    newUser.CrMasUserInformationCode, null, null, null, null, null, null, null, null, "اضافة",
                    "Insert", "I", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Employees", "Employees");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "0";

            //To Set Title 
            var titles = await setTitle("206", "2206001", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var user = await _userService.GetUserByUserNameAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Employees");
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var lastUser = _userManager.Users.ToList().LastOrDefault(x => x.CrMasUserInformationLessor == currentUser.CrMasUserInformationLessor);
            var crMasUserInformation = _mapper.Map<RegisterViewModel>(user);
            ViewBag.CreditLimit = crMasUserInformation.CrMasUserInformationCreditLimit?.ToString("N2", CultureInfo.InvariantCulture);
            crMasUserInformation.CrMasUserBranchValidities = _unitOfWork.CrMasUserBranchValidity.FindAll(x => x.CrMasUserBranchValidityId == user.CrMasUserInformationCode).ToList();
            crMasUserInformation.CrCasBranchInformations = _unitOfWork.CrCasBranchInformation.FindAll(x => x.CrCasBranchInformationLessor == currentUser.CrMasUserInformationLessor&&x.CrCasBranchInformationStatus!=Status.Deleted).ToList();
            return View(crMasUserInformation);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RegisterViewModel model,string CreditLimit, bool CrMasUserInformationAuthorizationAdmin, bool CrMasUserInformationAuthorizationOwner, bool CrMasUserInformationAuthorizationBranch, List<string> CheckboxBranchesWithData)
        {
            if (ModelState.IsValid)
            {

                var user = await _userService.GetUserByUserNameAsync(model.CrMasUserInformationCode);
                if (user != null)
                {
                    user.CrMasUserInformationTasksArName = model.CrMasUserInformationTasksArName;
                    user.CrMasUserInformationTasksEnName = model.CrMasUserInformationTasksEnName;
                    user.CrMasUserInformationCreditLimit = decimal.Parse(CreditLimit, CultureInfo.InvariantCulture);
                    user.CrMasUserInformationReasons = model.CrMasUserInformationReasons;
                    user.CrMasUserInformationAuthorizationAdmin = CrMasUserInformationAuthorizationAdmin;
                    user.CrMasUserInformationAuthorizationOwner = CrMasUserInformationAuthorizationOwner;

                    List<CheckboxBranchesAuthData> checkboxDataList = new List<CheckboxBranchesAuthData>();

                    // Deserialize and filter the checkbox data
                    foreach (var item in CheckboxBranchesWithData)
                    {
                        List<CheckboxBranchesAuthData> deserializedData = JsonConvert.DeserializeObject<List<CheckboxBranchesAuthData>>(item);
                        checkboxDataList.AddRange(deserializedData);
                    }

                    foreach (var item in checkboxDataList)
                    {
                        var id = item.Id;
                        var value = item.Value;
                        if (CrMasUserInformationAuthorizationBranch == true&&(value.ToLower() == "on" || value.ToLower() == "true"))
                        {
                            await _userBranchValidity.UpdateUserBranchValidity(user.CrMasUserInformationCode, user.CrMasUserInformationLessor, id, Status.Active);
                        }
                        else
                        {
                            await _userBranchValidity.UpdateUserBranchValidity(user.CrMasUserInformationCode, user.CrMasUserInformationLessor, id, Status.Deleted);
                        }

                    }
                    var BranchValidity= _unitOfWork.CrMasUserBranchValidity.FindAll(x=>x.CrMasUserBranchValidityId==user.CrMasUserInformationCode&&
                                                                                       x.CrMasUserBranchValidityLessor==user.CrMasUserInformationLessor&&
                                                                                       x.CrMasUserBranchValidityBranchStatus==Status.Active);
                    if (BranchValidity.Count()<1) user.CrMasUserInformationAuthorizationBranch = false;
                    else user.CrMasUserInformationAuthorizationBranch = true;
                    await _userService.UpdateAsync(user);
                    //SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("206", "2206001", "2");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                    // Save Adminstrive Procedures
                    await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "234", "20", currentUser.CrMasUserInformationLessor, "100",
                        user.CrMasUserInformationCode, null, null, null, null, null, null, null, null, "تعديل",
                       "Edit", "U", null);
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                    return RedirectToAction("Employees", "Employees");

                }
            }
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var user = await _userService.GetUserByUserNameAsync(code);
            if (user != null)
            {
                if (status == Status.Hold)
                {
                    sAr = "ايقاف موظف";
                    sEn = "Hold Employee";
                    user.CrMasUserInformationStatus = Status.Hold;
                }
                else if (status == Status.Deleted)
                {
                    sAr = "حذف موظف";
                    sEn = "Remove Employee";
                    user.CrMasUserInformationStatus = Status.Deleted;
                }
                else if (status == Status.Active)
                {
                    sAr = "استرجاع موظف";
                    sEn = "Retrive Employee";
                    user.CrMasUserInformationStatus = Status.Active;
                }
                await _unitOfWork.CompleteAsync();
                // SaveTracing
                var (mainTask, subTask, system, currentUser) = await SetTrace("206", "2206001", "2");
                await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                // Save Adminstrive Procedures
                await _adminstritiveProcedures.SaveAdminstritive(currentUser.CrMasUserInformationCode, "1", "234", "20", currentUser.CrMasUserInformationLessor, "100",
                    user.CrMasUserInformationCode, null, null, null, null, null, null, null, null, sAr, sEn, "U", null);
                _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                return RedirectToAction("Employees", "Employees");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string code)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var Employee= _unitOfWork.CrMasUserInformation.Find(x=>x.CrMasUserInformationCode==code&&x.CrMasUserInformationLessor== currentUser.CrMasUserInformationLessor);
            if (Employee!=null)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(Employee, Employee.CrMasUserInformationPassWord, Employee.CrMasUserInformationCode);
                if (changePasswordResult.Succeeded) {
                    Employee.CrMasUserInformationPassWord = Employee.CrMasUserInformationCode;
                    await _unitOfWork.CompleteAsync();
                    return Json(true);
                }
            }

            return Json(false);
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeSystemValiditions()
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "1";
            // Set Title
            var titles = await setTitle("206", "2206002", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);

            if (userLessor == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Exclude the current user from the list
            var usersByLessor = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);
            var userWithOutManger = usersByLessor.Where(x => x.CrMasUserInformationCode != ("CAS" + userLessor.CrMasUserInformationLessor));
            return View(userWithOutManger.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode && x.CrMasUserInformationStatus == Status.Active && x.CrMasUserInformationAuthorizationAdmin == true).ToList());
        }

        [HttpGet]
        public async Task<ActionResult> EditEmployeeSystemValiditions(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "1";

            // Set Title
            var titles = await setTitle("206", "2206002", "2");
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
        public async Task<IActionResult> EditEmployeeSystemValiditions([FromBody] CheckBoxModels model)
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
                    if (subTask != null) subTask.CrMasUserSubValidationAuthorization = checkboxSub.value;
                }
                else
                {
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
                        if (checkboxProcedure.procedureName.ToLower() == "insert" || checkboxProcedure.procedureName == "Car show for sale") procedureTask.CrMasUserProceduresValidationInsertAuthorization = checkboxProcedure.value;
                        if (checkboxProcedure.procedureName.ToLower() == "update" || checkboxProcedure.procedureName == "Cancel Offer To Sell") procedureTask.CrMasUserProceduresValidationUpDateAuthorization = checkboxProcedure.value;
                        if (checkboxProcedure.procedureName.ToLower() == "hold" || checkboxProcedure.procedureName == "Sale Execution") procedureTask.CrMasUserProceduresValidationHoldAuthorization = checkboxProcedure.value;
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
            var user = await _unitOfWork.CrMasUserInformation.FindAsync(x => x.CrMasUserInformationCode == model.UserId);

            //Save Adminstrive Procedures
            await _adminstritiveProcedures.SaveAdminstritive(user.CrMasUserInformationCode, "1", "234", "20", user.CrMasUserInformationLessor, "100",
           user.CrMasUserInformationCode, null, null, null, null, null, null, null, null, "تحديث صلاحيات العقد", "edit contract Validity", "U", null);
            await _unitOfWork.CompleteAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeContractValiditions()
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "2";
            // Set Title
            var titles = await setTitle("206", "2206003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var user = User; // Get the current User object
            var userLessor = await _userService.GetUserLessor(user);

            if (userLessor == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Exclude the current user from the list
            var usersByLessor = await _userService.GetAllUsersByLessor(userLessor.CrMasUserInformationLessor);
            var userWithOutManger = usersByLessor.Where(x => x.CrMasUserInformationCode != ("CAS" + userLessor.CrMasUserInformationLessor));
            return View(userWithOutManger.Where(x => x.CrMasUserInformationCode != userLessor.CrMasUserInformationCode && x.CrMasUserInformationStatus == Status.Active && x.CrMasUserInformationAuthorizationBranch == true).ToList());
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployeeContractValiditions(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarUsers";
            ViewBag.no = "2";

            // Set Title
            var titles = await setTitle("206", "2206003", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);
            var user = await _userService.GetUserByUserNameAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View();
            }
            var contractValidtion = _unitOfWork.CrMasUserContractValidity.Find(x => x.CrMasUserContractValidityUserId == user.Id);
            if (contractValidtion == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View();
            }

            var model = _mapper.Map<ContractValiditionsVM>(contractValidtion);
            model.CrMasUserContractValidityUser = user;
            model.CrMasSysProcedure = _unitOfWork.CrMasSysProcedure.FindAll(x => x.CrMasSysProceduresStatus == Status.Active && (x.CrMasSysProceduresClassification == "10" || x.CrMasSysProceduresClassification == "11" || x.CrMasSysProceduresClassification == "12" || x.CrMasSysProceduresClassification == "13")).ToList();
            model.CrCasLessorMechanism = _unitOfWork.CrCasLessorMechanism.FindAll(x => x.CrCasLessorMechanismCode == user.CrMasUserInformationLessor && (x.CrCasLessorMechanismProceduresClassification == "10" || x.CrCasLessorMechanismProceduresClassification == "11" || x.CrCasLessorMechanismProceduresClassification == "12" || x.CrCasLessorMechanismProceduresClassification == "13")).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployeeContractValiditions(ContractValiditionsVM contractValiditionsVM)
        {
            var user = await _userService.GetUserByUserNameAsync(contractValiditionsVM.CrMasUserContractValidityUserId);

            var contractValidition = _unitOfWork.CrMasUserContractValidity.Find(x => x.CrMasUserContractValidityUserId == contractValiditionsVM.CrMasUserContractValidityUserId);
            if (contractValidition == null) _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            var model = _mapper.Map<CrMasUserContractValidity>(contractValiditionsVM);
            if (await _userContractValididation.EditContractValiditionsForEmployee(model)) _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            // Save Adminstrive Procedures
            await _adminstritiveProcedures.SaveAdminstritive(user.CrMasUserInformationCode, "1", "234", "20", user.CrMasUserInformationLessor, "100",
                user.CrMasUserInformationCode, null, null, null, null, null, null, null, null, "تحديث صلاحيات العقد", "edit contract Validity", "U", null);
            return RedirectToAction("EmployeeContractValiditions");
        }

        [HttpGet]
        public async Task<IActionResult> MyAccount()
        {
            // Set Title
            var titles = await setTitle("206", "2206001", "2");
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
        public async Task<IActionResult> MyAccount(RegisterViewModel model, IFormFile UserSignatureFile, IFormFile UserImgFile)
        {
            var user = await _userService.GetUserByUserNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Login", "Account");

            string foldername = $"{"images\\Company"}\\{user.CrMasUserInformationLessor}\\{"Users"}\\{model.CrMasUserInformationCode}";
            string filePathImage;
            string filePathSignture;
            string oldPathImage = user.CrMasUserInformationPicture;
            string oldPathSignture = user.CrMasUserInformationPicture;
            if (oldPathImage == "~/images/common/user.jpg") oldPathImage = "";
            if (oldPathSignture == "~/images/common/DefualtUserSignature.png") oldPathSignture = "";
            if (UserImgFile != null)
            {
                string fileNameImg = "Image_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقت
                filePathImage = await UserImgFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png", oldPathImage);
            }
            else
            {
                filePathImage = "~/images/common/user.jpg";
            }
            if (UserSignatureFile != null)
            {
                string fileNameSignture = "Signture_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقت
                filePathSignture = await UserSignatureFile.SaveImageAsync(_webHostEnvironment, foldername, fileNameSignture, ".png", oldPathSignture);
            }
            else
            {
                filePathSignture = "~/images/common/DefualtUserSignature.png";
            }
            user.CrMasUserInformationCallingKey = model.CrMasUserInformationCallingKey;
            user.CrMasUserInformationMobileNo = model.CrMasUserInformationMobileNo;
            user.CrMasUserInformationEmail = model.CrMasUserInformationEmail;
            user.CrMasUserInformationExitTimer = model.CrMasUserInformationExitTimer;
            user.CrMasUserInformationDefaultLanguage = model.CrMasUserInformationDefaultLanguage;
            user.CrMasUserInformationRemindMe = model.CrMasUserInformationRemindMe;
            user.CrMasUserInformationSignature = filePathSignture;
            user.CrMasUserInformationPicture = filePathImage;
            await _userManager.UpdateAsync(user);
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
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
                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                }
            }
            else
            {
                ModelState.AddModelError("Exist", "Something wrong happed , please try again");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SuccessToast()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("EmployeeSystemValiditions", "Employees");
        }
        public IActionResult SuccessResetPassword()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Employees", "Employees");
        }
    }
}
