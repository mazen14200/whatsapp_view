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

        public LessorImagesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<LessorImagesController> localizer, IToastNotification toastNotification , IUserLoginsService userLoginsService) : base(userManager, unitOfWork, mapper)
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
                    var NewName = Name+"_"+ DateTime.Now.ToString("yyyyMMddHHmmss"); // اسم مبني على التاريخ والوقت
                    var file = item;
                    if (file != null && Name != null)
                    {
                        
                        if (Name == "CompanyLogo")lessorImages.CrMasLessorImageLogo = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageLogo);
                        if (Name == "LogoPrint") lessorImages.CrMasLessorImageLogoPrint = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageLogoPrint);
                        if (Name == "Stamp") lessorImages.CrMasLessorImageStamp = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStamp);
                        if (Name == "StampOutsideCity") lessorImages.CrMasLessorImageStampOutsideCity = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStampOutsideCity);
                        if (Name == "StampOutsideCountry") lessorImages.CrMasLessorImageStampOutsideCountry = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStampOutsideCountry);
                        if (Name == "StampFullAmountPaid") lessorImages.CrMasLessorImageStampFullAmountPaid = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageStampFullAmountPaid);
                        if (Name == "SignatureDirector") lessorImages.CrMasLessorImageSignatureDirector = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageSignatureDirector);
                        if (Name == "CreateContractEmail") lessorImages.CrMasLessorImageCreateContractEmail = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageCreateContractEmail);
                        if (Name == "CreateContractWhatUp") lessorImages.CrMasLessorImageCreateContractWhatUp = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageCreateContractWhatUp);
                        if (Name == "TomorrowContractEmail") lessorImages.CrMasLessorImageTomorrowContractEmail = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageTomorrowContractEmail);
                        if (Name == "TomorrowContractWhatUp") lessorImages.CrMasLessorImageTomorrowContractWhatUp = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageTomorrowContractWhatUp);
                        if (Name == "HourContractEmail") lessorImages.CrMasLessorImageHourContractEmail = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageHourContractEmail);
                        if (Name == "HourContractWhatUp") lessorImages.CrMasLessorImageHourContractWhatUp = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageHourContractWhatUp);
                        if (Name == "EndContractEmail") lessorImages.CrMasLessorImageEndContractEmail = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEndContractEmail);
                        if (Name == "EndContractWhatUp") lessorImages.CrMasLessorImageEndContractWhatUp = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageEndContractWhatUp);
                        if (Name == "CloseContractEmail") lessorImages.CrMasLessorImageCloseContractEmail = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageCloseContractEmail);
                        if (Name == "CloseContractWhatUp") lessorImages.CrMasLessorImageCloseContractWhatUp = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageCloseContractWhatUp);
                        if (Name == "ContArConditions1") lessorImages.CrMasLessorImageContArConditions1 = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContArConditions1);
                        if (Name == "ContArConditions2") lessorImages.CrMasLessorImageContArConditions2 = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContArConditions2);
                        if (Name == "ContArConditions3") lessorImages.CrMasLessorImageContArConditions3 = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContArConditions3);
                        if (Name == "ContEnConditions1") lessorImages.CrMasLessorImageContEnConditions1 = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContEnConditions1);
                        if (Name == "ContEnConditions2") lessorImages.CrMasLessorImageContEnConditions2 = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContEnConditions2);
                        if (Name == "ContEnConditions3") lessorImages.CrMasLessorImageContEnConditions3 = await file.SaveImageAsync(_webHostEnvironment, foldername, NewName, ".png", lessorImages.CrMasLessorImageContEnConditions3);

                    }
                    _unitOfWork.CrMasLessorImage.Update(lessorImages);


                    // SaveTracing
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
                            var deleteImage = await FileExtensions.RemoveImage(_webHostEnvironment, foldername, id, ".png");
                            if (id == "CompanyLogo") lessorImages.CrMasLessorImageLogo = null;
                            if (id == "LogoPrint") lessorImages.CrMasLessorImageLogoPrint = null;
                            if (id == "Stamp") lessorImages.CrMasLessorImageStamp = null;
                            if (id == "StampOutsideCity") lessorImages.CrMasLessorImageStampOutsideCity = null;
                            if (id == "StampOutsideCountry") lessorImages.CrMasLessorImageStampOutsideCountry = null;
                            if (id == "StampFullAmountPaid") lessorImages.CrMasLessorImageStampFullAmountPaid = null;
                            if (id == "SignatureDirector") lessorImages.CrMasLessorImageSignatureDirector = null;
                            if (id == "CreateContractEmail") lessorImages.CrMasLessorImageCreateContractEmail = null;
                            if (id == "CreateContractWhatUp") lessorImages.CrMasLessorImageCreateContractWhatUp = null;
                            if (id == "TomorrowContractEmail") lessorImages.CrMasLessorImageTomorrowContractEmail = null;
                            if (id == "TomorrowContractWhatUp") lessorImages.CrMasLessorImageTomorrowContractWhatUp = null;
                            if (id == "HourContractEmail") lessorImages.CrMasLessorImageHourContractEmail = null;
                            if (id == "HourContractWhatUp") lessorImages.CrMasLessorImageHourContractWhatUp = null;
                            if (id == "EndContractEmail") lessorImages.CrMasLessorImageEndContractEmail = null;
                            if (id == "EndContractWhatUp") lessorImages.CrMasLessorImageEndContractWhatUp = null;
                            if (id == "CloseContractEmail") lessorImages.CrMasLessorImageCloseContractEmail = null;
                            if (id == "CloseContractWhatUp") lessorImages.CrMasLessorImageCloseContractWhatUp = null;
                            if (id == "ContArConditions1") lessorImages.CrMasLessorImageContArConditions1 = null;
                            if (id == "ContArConditions2") lessorImages.CrMasLessorImageContArConditions2 = null;
                            if (id == "ContArConditions3") lessorImages.CrMasLessorImageContArConditions3 = null;
                            if (id == "ContEnConditions1") lessorImages.CrMasLessorImageContEnConditions1 = null;
                            if (id == "ContEnConditions2") lessorImages.CrMasLessorImageContEnConditions2 = null;
                            if (id == "ContEnConditions3") lessorImages.CrMasLessorImageContEnConditions3 = null;

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