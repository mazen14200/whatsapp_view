using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.Areas.CAS.Controllers;
using Bnan.Ui.ViewModels.MAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using NToastNotify;

namespace Bnan.Ui.Areas.MAS.Controllers
{
    [Area("MAS")]
    [Authorize(Roles = "MAS")]
    public class LessorImagesController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<LessorImagesController> _localizer;
        private readonly IToastNotification _toastNotification;
        private readonly IUserLoginsService _userLoginsService;

        public LessorImagesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<LessorImagesController> localizer, IToastNotification toastNotification, IUserLoginsService userLoginsService) : base(userManager, unitOfWork, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
            _toastNotification = toastNotification;
            _userLoginsService = userLoginsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101002", "1");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);



            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "1";

            // Set Title
            var titles = await setTitle("101", "1101002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            //Check User Sub Validation
            var UserValidation = await CheckUserSubValidation("1101002");
            if (UserValidation == false) return RedirectToAction("Index", "Home", new { area = "MAS" });

            var Lessors = _unitOfWork.CrMasLessorInformation.FindAll(l => l.CrMasLessorInformationCode != "0000" && l.CrMasLessorInformationStatus == Status.Active);
            return View(Lessors);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            //sidebar Active
            ViewBag.id = "#sidebarCompany";
            ViewBag.no = "1";

            var lessor = _unitOfWork.CrMasLessorInformation.Find(x => x.CrMasLessorInformationCode == id);

            var lessorimgs = _unitOfWork.CrMasLessorImage.Find(x => x.CrMasLessorImageCode == lessor.CrMasLessorInformationCode);
            var lessorVM = _mapper.Map<LessorImagesVM>(lessorimgs);
            lessorVM.CrMasLessorNameAr = lessor.CrMasLessorInformationArLongName;
            lessorVM.CrMasLessorNameEn = lessor.CrMasLessorInformationEnLongName;
            // Set Title
            var titles = await setTitle("101", "1101002", "1");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تعديل", "Update", titles[3]);
            return View(lessorVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditImages(IFormCollection formData)
        {
            string lessorCode = Request.Headers["lessorcode"].ToString();

            var lessorImages = _unitOfWork.CrMasLessorImage.GetById(lessorCode);
            if (lessorImages != null)
            {
                string foldername = $"{"images\\Company"}\\{lessorImages.CrMasLessorImageCode}\\{"Support Images"}";
                foreach (var item in formData.Files)
                {
                    var Name = item.Name;
                    var NewName = Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقت
                    var file = item;
                    if (file != null && Name != null)
                    {

                        if (Name == "CompanyLogo") lessorImages.CrMasLessorImageLogo = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageLogo);
                        if (Name == "Stamp") lessorImages.CrMasLessorImageStamp = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStamp);
                        if (Name == "StampOutsideCity") lessorImages.CrMasLessorImageStampOutsideCity = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStampOutsideCity);
                        if (Name == "StampOutsideCountry") lessorImages.CrMasLessorImageStampOutsideCountry = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStampOutsideCountry);
                        if (Name == "StampFullAmountPaid") lessorImages.CrMasLessorImageStampFullAmountPaid = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStampFullAmountPaid);
                        if (Name == "SignatureDirector") lessorImages.CrMasLessorImageSignatureDirector = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageSignatureDirector);
                        if (Name == "ContractCard") lessorImages.CrMasLessorImageContractCard = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractCard);
                        if (Name == "ExtensionContractCard") lessorImages.CrMasLessorImageContractExtensionCard = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractExtensionCard);
                        if (Name == "Contract24HourCard") lessorImages.CrMasLessorImageContract24Hour = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContract24Hour);
                        if (Name == "Contract4Hour") lessorImages.CrMasLessorImageContract4Hour = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContract4Hour);
                        if (Name == "ContractFinished") lessorImages.CrMasLessorImageContractFinished = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractFinished);
                        if (Name == "ContractClosed") lessorImages.CrMasLessorImageContractClosed = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractClosed);
                        if (Name == "ContractCancelled") lessorImages.CrMasLessorImageContractCancelled = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractCancelled);
                        if (Name == "ArInitialInvoice") lessorImages.CrMasLessorImageArInitialInvoice = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArInitialInvoice);
                        if (Name == "ArActualInvoice") lessorImages.CrMasLessorImageArActualInvoice = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArActualInvoice);
                        if (Name == "ArExternalCatchReceipt") lessorImages.CrMasLessorImageArExternalCatchReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArExternalCatchReceipt);
                        if (Name == "ArExternalBillExchangeReceipt") lessorImages.CrMasLessorImageArExternalBillExchangeReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArExternalBillExchangeReceipt);
                        if (Name == "ArInternalCatchReceipt") lessorImages.CrMasLessorImageArInternalCatchReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArInternalCatchReceipt);
                        if (Name == "ArInternalBillExchangeReceipt") lessorImages.CrMasLessorImageArInternalBillExchangeReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArInternalBillExchangeReceipt);
                        if (Name == "EnInitialInvoice") lessorImages.CrMasLessorImageEnInitialInvoice = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnInitialInvoice);
                        if (Name == "EnActualInvoice") lessorImages.CrMasLessorImageEnActualInvoice = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnActualInvoice);
                        if (Name == "EnExternalCatchReceipt") lessorImages.CrMasLessorImageEnExternalCatchReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnExternalCatchReceipt);
                        if (Name == "EnExternalBillExchangeReceipt") lessorImages.CrMasLessorImageEnExternalBillExchangeReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnExternalBillExchangeReceipt);
                        if (Name == "EnInternalCatchReceipt") lessorImages.CrMasLessorImageEnInternalCatchReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnInternalCatchReceipt);
                        if (Name == "EnInternalBillExchangeReceipt") lessorImages.CrMasLessorImageEnInternalBillExchangeReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnInternalBillExchangeReceipt);


                    }
                    _unitOfWork.CrMasLessorImage.Update(lessorImages);


                    // SaveTracingb 
                    var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101002", "1");
                    var RecordAr = "";
                    var RecordEn = "";
                    RecordAr = _unitOfWork.CrMasLessorInformation.Find(x => x.CrMasLessorInformationCode == lessorImages.CrMasLessorImageCode).CrMasLessorInformationArShortName + " - " + _localizer[item.Name];
                    RecordEn = _unitOfWork.CrMasLessorInformation.Find(x => x.CrMasLessorInformationCode == lessorImages.CrMasLessorImageCode).CrMasLessorInformationEnShortName + " - " + _localizer[item.Name];
                    await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "إضافة صورة", "Add Image", mainTask.CrMasSysMainTasksCode,
                    subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                    subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


                }

                if (Request.Form.ContainsKey("imagesForRemove"))
                {
                    var imagesForRemoveJson = Request.Form["imagesForRemove"];
                    var imagesForRemove = JsonConvert.DeserializeObject<List<RemoveFileViewModel>>(imagesForRemoveJson);
                    if (imagesForRemove != null)
                    {
                        foreach (var image in imagesForRemove)
                        {
                            var id = image.id;
                            if (id == "CompanyLogo")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageLogo);
                                if (Tr == true) lessorImages.CrMasLessorImageLogo = null;
                            }
                            if (id == "Stamp")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageStamp);
                                if (Tr == true) lessorImages.CrMasLessorImageStamp = null;
                            }
                            if (id == "StampOutsideCity")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageStampOutsideCity);
                                if (Tr == true) lessorImages.CrMasLessorImageStampOutsideCity = null;
                            };
                            if (id == "StampOutsideCountry")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageStampOutsideCountry);
                                if (Tr == true) lessorImages.CrMasLessorImageStampOutsideCountry = null;
                            };
                            if (id == "StampFullAmountPaid")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageStampFullAmountPaid);
                                if (Tr == true) lessorImages.CrMasLessorImageStampFullAmountPaid = null;
                            }
                            if (id == "SignatureDirector")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageSignatureDirector);
                                if (Tr == true) lessorImages.CrMasLessorImageSignatureDirector = null;
                            }
                            if (id == "ContractCard")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageContractCard);
                                if (Tr == true) lessorImages.CrMasLessorImageContractCard = null;
                            }
                            if (id == "ContractExtensionCard")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageContractExtensionCard);
                                if (Tr == true) lessorImages.CrMasLessorImageContractExtensionCard = null;
                            }
                            if (id == "Contract24Hour")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageContract24Hour);
                                if (Tr == true) lessorImages.CrMasLessorImageContract24Hour = null;
                            }
                            if (id == "Contract4Hour")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageContract4Hour);
                                if (Tr == true) lessorImages.CrMasLessorImageContract4Hour = null;
                            }
                            if (id == "ContractFinished")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageContractFinished);
                                if (Tr == true) lessorImages.CrMasLessorImageContractFinished = null;
                            }
                            if (id == "ContractClosed")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageContractClosed);
                                if (Tr == true) lessorImages.CrMasLessorImageContractClosed = null;
                            }
                            if (id == "ContractCancelled")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageContractCancelled);
                                if (Tr == true) lessorImages.CrMasLessorImageContractCancelled = null;
                            }
                            if (id == "ArInitialInvoice")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArInitialInvoice);
                                if (Tr == true) lessorImages.CrMasLessorImageArInitialInvoice = null;
                            }
                            if (id == "ArActualInvoice")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArActualInvoice);
                                if (Tr == true) lessorImages.CrMasLessorImageArActualInvoice = null;
                            }

                            if (id == "ArExternalCatchReceipt")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArExternalCatchReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageArExternalCatchReceipt = null;
                            }
                            if (id == "ArExternalBillExchangeReceipt")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArExternalBillExchangeReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageArExternalBillExchangeReceipt = null;
                            }
                            if (id == "ArInternalCatchReceipt")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArInternalCatchReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageArInternalCatchReceipt = null;
                            }
                            if (id == "ArInternalBillExchangeReceipt") 
                            { 
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArInternalBillExchangeReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageArInternalBillExchangeReceipt = null;
                            }
                            if (id == "EnInitialInvoice")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnInitialInvoice);
                                if (Tr == true) lessorImages.CrMasLessorImageEnInitialInvoice = null;
                            }
                            if (id == "EnActualInvoice") { 
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnActualInvoice);
                                if (Tr == true) lessorImages.CrMasLessorImageEnActualInvoice = null;
                            }
                            if (id == "EnExternalCatchReceipt") {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnExternalCatchReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageEnExternalCatchReceipt = null;
                            }
                            if (id == "EnExternalBillExchangeReceipt") { 
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnExternalBillExchangeReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageEnExternalBillExchangeReceipt = null;
                            }
                            if (id == "EnInternalCatchReceipt") {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnInternalCatchReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageEnInternalCatchReceipt = null;
                            }
                            if (id == "EnInternalBillExchangeReceipt") {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnInternalBillExchangeReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageEnInternalBillExchangeReceipt = null;
                            }

                            _unitOfWork.CrMasLessorImage.Update(lessorImages);
                            // SaveTracing
                            var (mainTask, subTask, system, currentUser) = await SetTrace("101", "1101002", "1");
                            var RecordAr = "";
                            var RecordEn = "";
                            RecordAr = _unitOfWork.CrMasLessorInformation.Find(x => x.CrMasLessorInformationCode == lessorImages.CrMasLessorImageCode).CrMasLessorInformationArShortName + " - " + _localizer[image.id];
                            RecordEn = _unitOfWork.CrMasLessorInformation.Find(x => x.CrMasLessorInformationCode == lessorImages.CrMasLessorImageCode).CrMasLessorInformationEnShortName + " - " + _localizer[image.id];
                            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, RecordAr, RecordEn, "حذف صورة", "Delete Image", mainTask.CrMasSysMainTasksCode,
                            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
                            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);
                        }
                    }

                }
                _unitOfWork.Complete();
                return Json(true);
            }
            return Json(false);

        }
        public IActionResult SuccesssMessageForLessorImages()
        {
            _toastNotification.AddSuccessToastMessage(_localizer["ToastEdit"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            return RedirectToAction("Index");
        }

    }
}