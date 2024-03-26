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
                        if (Name == "Contract24Hour") lessorImages.CrMasLessorImageContract24Hour = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContract24Hour);
                        if (Name == "Contract4Hour") lessorImages.CrMasLessorImageContract4Hour = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContract4Hour);
                        if (Name == "ContractFinished") lessorImages.CrMasLessorImageContractFinished = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractFinished);
                        if (Name == "ContractClosed") lessorImages.CrMasLessorImageContractClosed = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractClosed);
                        if (Name == "ContractCancelled") lessorImages.CrMasLessorImageContractCancelled = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContractCancelled);
                        if (Name == "ArCatchReceipt") lessorImages.CrMasLessorImageArCatchReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArCatchReceipt);
                        if (Name == "ArBillExchange") lessorImages.CrMasLessorImageArBillExchange = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArBillExchange);
                        if (Name == "ArBoxFeeding") lessorImages.CrMasLessorImageArBoxFeeding = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArBoxFeeding);
                        if (Name == "ArDeliveryCustody") lessorImages.CrMasLessorImageArDeliveryCustody = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArDeliveryCustody);
                        if (Name == "ArTransferFtenant") lessorImages.CrMasLessorImageArTransferFtenant = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArTransferFtenant);
                        if (Name == "ArTransferTtenant") lessorImages.CrMasLessorImageArTransferTtenant = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageArTransferTtenant);
                        if (Name == "EnCatchReceipt") lessorImages.CrMasLessorImageEnCatchReceipt = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnCatchReceipt);
                        if (Name == "EnBillExchange") lessorImages.CrMasLessorImageEnBillExchange = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnBillExchange);
                        if (Name == "EnBoxFeeding") lessorImages.CrMasLessorImageEnBoxFeeding = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnBoxFeeding);
                        if (Name == "EnDeliveryCustody") lessorImages.CrMasLessorImageEnDeliveryCustody = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnDeliveryCustody);
                        if (Name == "EnTransferFtenant") lessorImages.CrMasLessorImageEnTransferFtenant = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnTransferFtenant);
                        if (Name == "EnTransferTtenant") lessorImages.CrMasLessorImageEnTransferTtenant = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEnTransferTtenant);


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
                            if (id == "ArCatchReceipt")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArCatchReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageArCatchReceipt = null;
                            }
                            if (id == "ArBillExchange")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArBillExchange);
                                if (Tr == true) lessorImages.CrMasLessorImageArBillExchange = null;
                            }

                            if (id == "ArBoxFeeding")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArBoxFeeding);
                                if (Tr == true) lessorImages.CrMasLessorImageArBoxFeeding = null;
                            }
                            if (id == "ArDeliveryCustody")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArDeliveryCustody);
                                if (Tr == true) lessorImages.CrMasLessorImageArDeliveryCustody = null;
                            }
                            if (id == "ArTransferFtenant")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArTransferFtenant);
                                if (Tr == true) lessorImages.CrMasLessorImageArTransferFtenant = null;
                            }
                            if (id == "ArTransferTtenant") 
                            { 
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageArTransferTtenant);
                                if (Tr == true) lessorImages.CrMasLessorImageArTransferTtenant = null;
                            }
                            if (id == "EnCatchReceipt")
                            {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnCatchReceipt);
                                if (Tr == true) lessorImages.CrMasLessorImageEnCatchReceipt = null;
                            }
                            if (id == "EnBillExchange") { 
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnBillExchange);
                                if (Tr == true) lessorImages.CrMasLessorImageEnBillExchange = null;
                            }
                            if (id == "EnBoxFeeding") {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnBoxFeeding);
                                if (Tr == true) lessorImages.CrMasLessorImageEnBoxFeeding = null;
                            }
                            if (id == "EnDeliveryCustody") { 
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnDeliveryCustody);
                                if (Tr == true) lessorImages.CrMasLessorImageEnDeliveryCustody = null;
                            }
                            if (id == "EnTransferFtenant") {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnTransferFtenant);
                                if (Tr == true) lessorImages.CrMasLessorImageEnTransferFtenant = null;
                            }
                            if (id == "EnTransferTtenant") {
                                var Tr = await FileExtensions.RemoveImage(_webHostEnvironment, lessorImages.CrMasLessorImageEnTransferTtenant);
                                if (Tr == true) lessorImages.CrMasLessorImageEnTransferTtenant = null;
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