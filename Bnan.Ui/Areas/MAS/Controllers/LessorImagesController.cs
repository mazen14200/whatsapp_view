using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
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
        public LessorImagesController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<LessorImagesController> localizer, IToastNotification toastNotification) : base(userManager, unitOfWork, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _localizer = localizer;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
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
                    var file = item;
                    if (file != null && Name != null)
                    {
                        var saveImage = await file.SaveImageAsync(_webHostEnvironment, foldername, Name, ".png");
                        if (Name == "CompanyLogo") lessorImages.CrMasLessorImageLogo = saveImage;
                        if (Name == "LogoPrint") lessorImages.CrMasLessorImageLogoPrint = saveImage;
                        if (Name == "Stamp") lessorImages.CrMasLessorImageStamp = saveImage;
                        if (Name == "StampOutsideCity") lessorImages.CrMasLessorImageStampOutsideCity = saveImage;
                        if (Name == "StampOutsideCountry") lessorImages.CrMasLessorImageStampOutsideCountry = saveImage;
                        if (Name == "StampFullAmountPaid") lessorImages.CrMasLessorImageStampFullAmountPaid = saveImage;
                        if (Name == "SignatureDirector") lessorImages.CrMasLessorImageSignatureDirector = saveImage;
                        if (Name == "CreateContractEmail") lessorImages.CrMasLessorImageCreateContractEmail = saveImage;
                        if (Name == "CreateContractWhatUp") lessorImages.CrMasLessorImageCreateContractWhatUp = saveImage;
                        if (Name == "TomorrowContractEmail") lessorImages.CrMasLessorImageTomorrowContractEmail = saveImage;
                        if (Name == "TomorrowContractWhatUp") lessorImages.CrMasLessorImageTomorrowContractWhatUp = saveImage;
                        if (Name == "HourContractEmail") lessorImages.CrMasLessorImageHourContractEmail = saveImage;
                        if (Name == "HourContractWhatUp") lessorImages.CrMasLessorImageHourContractWhatUp = saveImage;
                        if (Name == "EndContractEmail") lessorImages.CrMasLessorImageEndContractEmail = saveImage;
                        if (Name == "EndContractWhatUp") lessorImages.CrMasLessorImageEndContractWhatUp = saveImage;
                        if (Name == "CloseContractEmail") lessorImages.CrMasLessorImageCloseContractEmail = saveImage;
                        if (Name == "CloseContractWhatUp") lessorImages.CrMasLessorImageCloseContractWhatUp = saveImage;
                        if (Name == "ContArConditions1") lessorImages.CrMasLessorImageContArConditions1 = saveImage;
                        if (Name == "ContArConditions2") lessorImages.CrMasLessorImageContArConditions2 = saveImage;
                        if (Name == "ContArConditions3") lessorImages.CrMasLessorImageContArConditions3 = saveImage;
                        if (Name == "ContEnConditions1") lessorImages.CrMasLessorImageContEnConditions1 = saveImage;
                        if (Name == "ContEnConditions2") lessorImages.CrMasLessorImageContEnConditions2 = saveImage;
                        if (Name == "ContEnConditions3") lessorImages.CrMasLessorImageContEnConditions3 = saveImage;
                    }
                    _unitOfWork.CrMasLessorImage.Update(lessorImages);
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