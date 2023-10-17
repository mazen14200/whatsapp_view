using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NToastNotify;
using System.Globalization;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class PaymentController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IPaymentMethods _paymentMethods;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<PaymentController> _localizer;
        private readonly IToastNotification _toastNotification;


        public PaymentController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
            IMapper mapper,IUserService userService, IPaymentMethods paymentMethods, 
            IUserLoginsService userLoginsService, IWebHostEnvironment webHostEnvironment, IToastNotification toastNotification
            , IStringLocalizer<PaymentController> localizer) : 
            base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _paymentMethods = paymentMethods;
            _userLoginsService = userLoginsService;
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
            _toastNotification = toastNotification;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var titles = await setTitle("109", "1109002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var methods = await _unitOfWork.CrMasSupAccountPaymentMethod.GetAllAsync();
            var PaymentFiltered = methods.Where(x => x.CrMasSupAccountPaymentMethodCode != "12" && x.CrMasSupAccountPaymentMethodCode != "11" && x.CrMasSupAccountPaymentMethodCode != "10"&& x.CrMasSupAccountPaymentMethodStatus == "A").ToList();

            return View(PaymentFiltered);
        }

        [HttpGet]
        public PartialViewResult GetPaymentByStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == Status.All)
                {
                    var PaymentbyStatusAll = _unitOfWork.CrMasSupAccountPaymentMethod.GetAll();
                    var PaymentbyStatusAllFilter = PaymentbyStatusAll.Where(x => x.CrMasSupAccountPaymentMethodCode != "12" && x.CrMasSupAccountPaymentMethodCode!="11"&&x.CrMasSupAccountPaymentMethodCode!="10").ToList();
                    return PartialView("_DataTablePayment", PaymentbyStatusAllFilter);
                }
                var PaymentbyStatus = _unitOfWork.CrMasSupAccountPaymentMethod.FindAll(l => l.CrMasSupAccountPaymentMethodStatus == status).ToList();
                var PaymentbyStatusFilter = PaymentbyStatus.Where(x => x.CrMasSupAccountPaymentMethodCode != "12" && x.CrMasSupAccountPaymentMethodCode != "11" && x.CrMasSupAccountPaymentMethodCode != "10").ToList();
                return PartialView("_DataTablePayment", PaymentbyStatusFilter);
            }
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //To Set Title !!!!!!!!!!!!!
            var titles = await setTitle("109", "1109002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Edit", titles[3]);

            var payment = await _unitOfWork.CrMasSupAccountPaymentMethod.GetByIdAsync(id);
            if (payment == null)
            {
                ModelState.AddModelError("Exist", "SomeThing Wrong is happened");
                return View("Index");
            }
            if (id == "10"||id=="12"||id=="11")
            {
                ModelState.AddModelError("Exist", "You Can't Edit This.");
                var payment2 = await _unitOfWork.CrMasSupAccountPaymentMethod.GetAllAsync();
                return View("Index", payment2);

            }
            var model = _mapper.Map<PaymentMethodsVM>(payment);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PaymentMethodsVM model , IFormFile? AcceptImg)
        {

            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            string foldername = $"{"images\\Common"}";
            string filePathImage = "";
            if (user != null)
            {
                if (model != null)
                {

                    if (AcceptImg != null)
                    {
                        string fileNameImg = model.CrMasSupAccountPaymentMethodEnName + "_" + model.CrMasSupAccountPaymentMethodCode;
                        filePathImage = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        model.CrMasSupAccountPaymentMethodAcceptImage = filePathImage;
                    }
                    //else
                    //{
                    //    model.CrMasSupAccountPaymentMethodAcceptImage = "~/images/Common/PaymentImg.png";
                    //}
                    var payment = _mapper.Map<CrMasSupAccountPaymentMethod>(model);

                    _unitOfWork.CrMasSupAccountPaymentMethod.Update(payment);
                    _unitOfWork.Complete();

                    // SaveTracing
                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "تعديل بيانات", "Edit information", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }

            }

            return RedirectToAction("Index", "Payment");
        }


        [HttpPost]
        public async Task<IActionResult> EditStatus(string code, string status)
        {
            string sAr = "";
            string sEn = "";
            var Payment = await _unitOfWork.CrMasSupAccountPaymentMethod.GetByIdAsync(code);
            if (Payment != null)
            {
                if (await CheckUserSubValidationProcdures("1109002", status))
                {
                    if (status == Status.Hold)
                    {
                        sAr = "ايقاف";
                        sEn = "Hold";
                        Payment.CrMasSupAccountPaymentMethodStatus = Status.Hold;
                    }
                    else if (status == Status.Deleted)
                    {
                        sAr = "حذف";
                        sEn = "Remove";
                        Payment.CrMasSupAccountPaymentMethodStatus = Status.Deleted;
                    }
                    else if (status == "Reactivate")
                    {
                        sAr = "استرجاع";
                        sEn = "Retrive";
                        Payment.CrMasSupAccountPaymentMethodStatus = Status.Active;
                    }

                    await _unitOfWork.CompleteAsync();

                    // SaveTracing

                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109002", "1");
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, sAr, sEn, mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    return RedirectToAction("Index", "Payment");
                }
            }

            return View(Payment);

        }

        [HttpGet]
        public async Task<IActionResult> AddPayment()
        {

            // Set Title !!!!!!!!!!!!!!!!!!!!!!!!!!
            var titles = await setTitle("109", "1109002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);


            var Payments = await _unitOfWork.CrMasSupAccountPaymentMethod.GetAllAsync();
            var PaymentsFiltered = Payments.Where(x => x.CrMasSupAccountPaymentMethodCode != "12" && x.CrMasSupAccountPaymentMethodCode != "11" && x.CrMasSupAccountPaymentMethodCode != "10").ToList();

            var PaymentCode = (int.Parse(PaymentsFiltered.LastOrDefault().CrMasSupAccountPaymentMethodCode) + 1).ToString();
            ViewBag.PaymentCode = PaymentCode;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentMethodsVM PaymentMethod,IFormFile? AcceptImg)
        {
            string foldername = $"{"images\\Common"}";
            string filePathImage="";

            string currentCulture = CultureInfo.CurrentCulture.Name;
            if (ModelState.IsValid)
            {
                if (PaymentMethod != null)
                {
                    var PaymentMethodVMT = _mapper.Map<CrMasSupAccountPaymentMethod>(PaymentMethod);
                    var payments = await _unitOfWork.CrMasSupAccountPaymentMethod.GetAllAsync();
                    var existingPayment = payments.FirstOrDefault(x =>
                        x.CrMasSupAccountPaymentMethodEnName == PaymentMethod.CrMasSupAccountPaymentMethodEnName ||
                        x.CrMasSupAccountPaymentMethodArName == PaymentMethod.CrMasSupAccountPaymentMethodArName);

                    // Generate code for the second time
                    var Payments = await _unitOfWork.CrMasSupAccountPaymentMethod.GetAllAsync();
                    var PaymentsCode = (int.Parse(Payments.LastOrDefault().CrMasSupAccountPaymentMethodCode) + 1).ToString();
                    PaymentMethod.CrMasSupAccountPaymentMethodCode = PaymentsCode;
                    ViewBag.PaymentCode = PaymentsCode;

                    if (existingPayment != null)
                    {
                        if (existingPayment.CrMasSupAccountPaymentMethodArName != null &&
                            existingPayment.CrMasSupAccountPaymentMethodArName== PaymentMethod.CrMasSupAccountPaymentMethodArName &&
                             existingPayment.CrMasSupAccountPaymentMethodEnName != PaymentMethod.CrMasSupAccountPaymentMethodEnName)
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
                        if(existingPayment.CrMasSupAccountPaymentMethodEnName != null &&
                            existingPayment.CrMasSupAccountPaymentMethodEnName == PaymentMethod.CrMasSupAccountPaymentMethodEnName &&
                             existingPayment.CrMasSupAccountPaymentMethodArName != PaymentMethod.CrMasSupAccountPaymentMethodArName)
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
                        if(existingPayment.CrMasSupAccountPaymentMethodArName != null && existingPayment.CrMasSupAccountPaymentMethodEnName!=null
                            && existingPayment.CrMasSupAccountPaymentMethodEnName == PaymentMethod.CrMasSupAccountPaymentMethodEnName &&
                           existingPayment.CrMasSupAccountPaymentMethodArName == PaymentMethod.CrMasSupAccountPaymentMethodArName)
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
                        return View(PaymentMethod);
                    }
                    if (AcceptImg != null)
                    {
                        string fileNameImg = PaymentMethod.CrMasSupAccountPaymentMethodEnName + "_" + PaymentMethod.CrMasSupAccountPaymentMethodCode;
                        filePathImage = await AcceptImg.SaveImageAsync(_webHostEnvironment, foldername, fileNameImg, ".png");
                        PaymentMethodVMT.CrMasSupAccountPaymentMethodAcceptImage = filePathImage;

                    }
                    //else
                    //{
                    //    PaymentMethodVMT.CrMasSupAccountPaymentMethodAcceptImage = "~/images/Common/PaymentImg.png";
                    //}
                    PaymentMethodVMT.CrMasSupAccountPaymentMethodStatus = "A";
                    PaymentMethodVMT.CrMasSupAccountPaymentMethodClassification = "2";

                    await _unitOfWork.CrMasSupAccountPaymentMethod.AddAsync(PaymentMethodVMT);

                    _unitOfWork.Complete();

                    var (mainTask, subTask, system, currentUser) = await SetTrace("109", "1109002", "1");

                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "اضافة طريقة دفع", "Add Payment Method", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);

                    _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });

                }
                return RedirectToAction("Index");
            }
            return View("AddPayment", PaymentMethod);
        }

    }
}
