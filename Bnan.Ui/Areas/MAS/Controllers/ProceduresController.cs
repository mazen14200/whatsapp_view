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

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class ProceduresController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        public ProceduresController(UserManager<CrMasUserInformation> userManager,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper, IUserLoginsService userLoginsService, IUserService userService) : base(userManager, unitOfWork, mapper)
        {
            _userLoginsService = userLoginsService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcedures()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("111", "1111005", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

            var Procedures = await _unitOfWork.CrMasSysProcedure.GetAllAsync();
            return View(Procedures);
        }

        [HttpGet]
        public PartialViewResult GetProceduresByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var ProcduresbyStatusAll = _unitOfWork.CrMasSysProcedure.GetAll();
                    return PartialView("_DataTableProcedures", ProcduresbyStatusAll);
                }
                var ProcduresbyStatus = _unitOfWork.CrMasSysProcedure.FindAll(l => l.CrMasSysProceduresStatus == status).ToList();
                return PartialView("_DataTableProcedures", ProcduresbyStatus);
            }
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Set Title
            var titles = await setTitle("111", "1111005", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var Procdures = await _unitOfWork.CrMasSysProcedure.GetAllAsync();
            var ProcduresCode = (int.Parse(Procdures.LastOrDefault().CrMasSysProceduresCode) + 1).ToString();
            ViewBag.ProcduresCode = ProcduresCode;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CrMasSysProcedureVM crMasSysProcedureVM)
        {
            if (ModelState.IsValid)
            {
                if (crMasSysProcedureVM != null)
                {
                    crMasSysProcedureVM.CrMasSysProceduresStatus = Status.Active;
                    var LessorVMTlessor = _mapper.Map<CrMasSysProcedure>(crMasSysProcedureVM);

                    await _unitOfWork.CrMasSysProcedure.AddAsync(LessorVMTlessor);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("111", "1111005", "1");
                    var RecordAr = LessorVMTlessor.CrMasSysProceduresArName;
                    var RecordEn = LessorVMTlessor.CrMasSysProceduresEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "اضافة", "Add", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                }
                var Procedures = await _unitOfWork.CrMasSysProcedure.GetAllAsync();
                return View("GetProcedures", Procedures);
            }
            return View("AddProcdure", crMasSysProcedureVM);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title 
            var titles = await setTitle("111", "1111001", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var procdure = await _unitOfWork.CrMasSysProcedure.GetByIdAsync(id);
            if (procdure == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Users");
            }
            var model = _mapper.Map<CrMasSysProcedureVM>(procdure);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CrMasSysProcedureVM CrMasSysProcedureVM)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            var procedure = _mapper.Map<CrMasSysProcedure>(CrMasSysProcedureVM);
            if (user != null)
            {
                if (procedure != null)
                {
                    _unitOfWork.CrMasSysProcedure.Update(procedure);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("111", "1111005", "1");
                    var RecordAr = procedure.CrMasSysProceduresArName;
                    var RecordEn = procedure.CrMasSysProceduresEnName;
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "تعديل", "Edit", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                }

            }
            return RedirectToAction("Index", "GetProcedures");
        }
    }
}
