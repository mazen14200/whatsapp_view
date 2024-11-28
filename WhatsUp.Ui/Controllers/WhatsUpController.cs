using AutoMapper;
using WhatsUp.Core.Extensions;
using WhatsUp.Core.Interfaces;
using WhatsUp.Core.Models;
using WhatsUp.Inferastructure.Extensions;
using WhatsUp.Inferastructure.Repository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;

using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Buffers.Text;
using System.Text;
using WhatsUp3.Ui.Models;
using static QRCoder.PayloadGenerator;
using ArrayToExcel;
using System;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml.Office2013.Word;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;
using Bnan.Core.Models;

namespace WhatsUp3.Ui.Controllers
{

    public static class BitMapExetnsion
    {
        public static byte[] ConvertBitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    public class MyItem
    {
        public MyItem Clone()
        {
            return (MyItem)this.MemberwiseClone();
        }
    }

    public class WhatsUpController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRenterIdType _RenterIdType;
        //private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<WhatsUpController> _localizer;


        public WhatsUpController( IUnitOfWork unitOfWork,
            IMapper mapper,  IRenterIdType RenterIdType,
            IWebHostEnvironment webHostEnvironment, IStringLocalizer<WhatsUpController> localizer) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RenterIdType = RenterIdType;
            //_toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var One_WhatsUp = _unitOfWork.Connect.GetAll().OrderBy(x => x.connectId).ToList();
            //var Account_ApiToken = _unitOfWork.AccountWhatsUp.Find(x => x.AccountWhatsUpCompanyId == "0000")?.AccountWhatsUpApiToken;
            //var MessageList = _unitOfWork.Message.FindAll(x => x.MessageStatus == "A").ToList();
            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Connect_List = One_WhatsUp;
            //whatsUpVM.Account_ApiToken = Account_ApiToken;
            //whatsUpVM.Message_List = MessageList;
            return View(whatsUpVM);
        }

        public async Task<IActionResult> websocketTest()
        {
            var One_WhatsUp = _unitOfWork.Connect.GetAll().OrderBy(x => x.connectId).ToList();
            //var Account_ApiToken = _unitOfWork.AccountWhatsUp.Find(x => x.AccountWhatsUpCompanyId == "0000")?.AccountWhatsUpApiToken;
            //var MessageList = _unitOfWork.Message.FindAll(x => x.MessageStatus == "A").ToList();
            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Connect_List = One_WhatsUp;
            //whatsUpVM.Account_ApiToken = Account_ApiToken;
            //whatsUpVM.Message_List = MessageList;
            return View(whatsUpVM);
        }

        
        [HttpGet]

        public async Task<IActionResult> Company_settings()
        {
            var One_WhatsUp = _unitOfWork.Company.FindAll(x => x.companyStatus == "A").ToList();
            //var connects = _unitOfWork.Connect.FindAll(x => x.connectStatus == "N" && x.connect_Login_Date_time == null).ToList();
            var connects = _unitOfWork.Connect.FindAll(x => x.connectStatus == "N").ToList();


            List<Company> List_filtered = new List<Company>();
            foreach (var connect in connects)
            {
                var One_setup = One_WhatsUp.Find(x => x.companyId == connect.connectId);
                if (One_setup != null)
                {
                    List_filtered.Add(One_setup);
                }
            }

            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Company_List = List_filtered;
            //whatsUpVM.Account_ApiToken = Account_ApiToken;

            return View(whatsUpVM);
        }

        [HttpGet]

        public async Task<IActionResult> Company_Disconnected()
        {
            var One_WhatsUp = _unitOfWork.Company.FindAll(x => x.companyStatus == "A").ToList();
            var connects = _unitOfWork.Connect.FindAll(x => x.connectStatus == "A" && x.connect_LogOut_Date_time == null).ToList();

            List<Company> List_filtered = new List<Company>();
            foreach (var connect in connects)
            {
                var One_setup = One_WhatsUp.Find(x => x.companyId == connect.connectId);
                if (One_setup != null)
                {
                    List_filtered.Add(One_setup);
                }
            }

            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Company_List = List_filtered;
            //whatsUpVM.Account_ApiToken = Account_ApiToken;

            return View(whatsUpVM);
        }

        

        [HttpGet]

        public async Task<IActionResult> add_Company()
        {
            var One_WhatsUp = _unitOfWork.Connect.GetAll().OrderBy(x => x.connectId).ToList();
            //var Account_ApiToken = _unitOfWork.AccountWhatsUp.Find(x=>x.AccountWhatsUpCompanyId=="0000")?.AccountWhatsUpApiToken;


            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Connect_List = One_WhatsUp;
            //whatsUpVM.Account_ApiToken = Account_ApiToken;

            return View(whatsUpVM);
        }

        

        [HttpGet]

        public async Task<IActionResult> Get_Message_FromThis_Connect(string connectId)
        {
            var messageList = _unitOfWork.Message.FindAll(x =>x.MessageConnectId== connectId && x.MessageStatus != "5").OrderByDescending(x => x.Message_Sent_DateTime).ToList();

            var result = new
            {
                messageList = messageList,
            };
            return Json(result);

        }


        [HttpGet]

        public async Task<IActionResult> Get_ConvertedNumber_Action(string our_No)
        {

            //var (ArConcatenate, EnConcatenate) = _convertedText.ConvertNumber(our_No, "Ar");

            var result = new
            {
                ar_concatenate = "",
                en_concatenate = "",
            };

            //return Json(new { code = 0 });
            return Json(result);
        }

        [HttpGet]

        public async Task<IActionResult> Generate_QR_code(string our_code_text)
        {
            QRCoder.QRCodeGenerator QRGen = new QRCoder.QRCodeGenerator();
            var QRData = QRGen.CreateQrCode(our_code_text , QRCoder.QRCodeGenerator.ECCLevel.H);
            QRCode qrCode = new QRCode(QRData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] BitmapArray = qrCodeImage.ConvertBitmapToByteArray();
            string url = string.Format("data:image/png;base64,{0}",Convert.ToBase64String(BitmapArray));
            ViewBag.ImageQrCode = url;

            var result = new
            {
                image = url,
                our_code_text = our_code_text,
            };

            //return Json(new { code = 0 });
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage_Page()
        {
            var One_WhatsUp = _unitOfWork.Company.FindAll(x => x.companyStatus == "A").ToList();
            var connects = _unitOfWork.Connect.FindAll(x => x.connectStatus == "A").ToList();
            
            List<Company> List_filtered = new List<Company>();
            foreach (var connect in connects)
            {
                var One_setup = One_WhatsUp.Find(x => x.companyId == connect.connectId);
                if (One_setup != null)
                {
                    List_filtered.Add(One_setup);
                }
            }
            //var Account_ApiToken = _unitOfWork.AccountWhatsUp.Find(x=>x.AccountWhatsUpCompanyId=="0000")?.AccountWhatsUpApiToken;

            var Renters = _unitOfWork.Renter.GetAll().OrderBy(x => x.RenterId).ToList();
            //var messages = _unitOfWork.Message.GetAll().ToList();

            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Company_List = List_filtered;
            whatsUpVM.Renter_List = Renters;
            //whatsUpVM.Message_List = messages;
            return View(whatsUpVM);
        }

        [HttpGet]
        public async Task<IActionResult> SendMessageMedia_Page()
        {
            var One_WhatsUp = _unitOfWork.Company.FindAll(x => x.companyStatus == "A").ToList();
            var connects = _unitOfWork.Connect.FindAll(x => x.connectStatus == "A").ToList();

            List<Company> List_filtered = new List<Company>();
            foreach (var connect in connects)
            {
                var One_setup = One_WhatsUp.Find(x => x.companyId == connect.connectId);
                if (One_setup != null)
                {
                    List_filtered.Add(One_setup);
                }
            }
            //var Account_ApiToken = _unitOfWork.AccountWhatsUp.Find(x=>x.AccountWhatsUpCompanyId=="0000")?.AccountWhatsUpApiToken;

            var Renters = _unitOfWork.Renter.GetAll().OrderBy(x => x.RenterId).ToList();
            //var messages = _unitOfWork.Message.GetAll().ToList();

            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Company_List = List_filtered;
            whatsUpVM.Renter_List = Renters;
            //whatsUpVM.Message_List = messages;
            return View(whatsUpVM);
        }


        [HttpGet("SendMessageExcel_Page")]
        public async Task<IActionResult> SendMessageExcel_Page()
        {
            var One_WhatsUp = _unitOfWork.Company.FindAll(x => x.companyStatus == "A").ToList();
            //var connects = _unitOfWork.Connect.FindAll(x => x.connectStatus == "A").ToList();
            var connects = _unitOfWork.Connect.GetAll().DistinctBy(x => x.connectId).ToList();

            List<Company> List_filtered = new List<Company>();
            foreach (var connect in connects)
            {
                var One_setup = One_WhatsUp.Find(x => x.companyId == connect.connectId);
                if (One_setup != null)
                {
                    List_filtered.Add(One_setup);
                }
            }
            //var Account_ApiToken = _unitOfWork.AccountWhatsUp.Find(x=>x.AccountWhatsUpCompanyId=="0000")?.AccountWhatsUpApiToken;

            var Renters = _unitOfWork.Renter.GetAll().OrderBy(x => x.RenterId).ToList();
            //var messages = _unitOfWork.Message.GetAll().ToList();

            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Company_List = List_filtered;
            whatsUpVM.Renter_List = Renters;
            //whatsUpVM.Message_List = messages;
            return View(whatsUpVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update_tables()
        {

            var postRegion = _unitOfWork.CrMasSupPostRegion.GetAll().ToList();
            var postRegion_x = _unitOfWork.CrMasSupPostRegion_x.GetAll().ToList();



            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.postRegion_old = postRegion;
            whatsUpVM.postRegion_updated = postRegion_x;
            return View(whatsUpVM);
            //var result = new
            //{
            //    code = 1,
            //    postRegion_old = postRegion,
            //    postRegion_updated = postRegion_x,
            //};
            //return Json(result);
        }


        
        [HttpGet]
        public async Task<IActionResult> get_Regions_updated_Totable_Action()
        {
            try
            {
                var postRegion = _unitOfWork.CrMasSupPostRegion.GetAll().ToList();
                var postRegion_x = _unitOfWork.CrMasSupPostRegion_x.GetAll().ToList();
                List<CrMasSupPostRegion> newList_add = new List<CrMasSupPostRegion>();
                List<CrMasSupPostRegion> newList_remove = new List<CrMasSupPostRegion>();

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<CrMasSupPostRegion, CrMasSupPostRegion_x>();
                    cfg.CreateMap<CrMasSupPostRegion_x, CrMasSupPostRegion>();

                });

                IMapper mapper = config.CreateMapper();

                //List<CrMasSupPostRegion> postRegion_x_list = postRegion_x
                //.Select(product => mapper.Map<CrMasSupPostRegion>(product))
                //.ToList();

                //var differentProducts = postRegion_x_list?.Except(postRegion).ToList();


                foreach (var single_x in postRegion_x)
                {
                   var one_region = postRegion.Find(x=>x.CrMasSupPostRegionsCode == single_x.CrMasSupPostRegionsCode);
                    if(one_region == null)
                    {
                        var newOne_x = mapper.Map<CrMasSupPostRegion>(single_x);
                        newList_add.Add(newOne_x);
                    }
                    else
                    {
                        //var newOne_x = mapper.Map<CrMasSupPostRegion>(single_x);

                        one_region.CrMasSupPostRegionsArName = single_x.CrMasSupPostRegionsArName;
                        one_region.CrMasSupPostRegionsEnName = single_x.CrMasSupPostRegionsEnName;
                        one_region.CrMasSupPostRegionsLocation = single_x.CrMasSupPostRegionsLocation;
                        one_region.CrMasSupPostRegionsLatitude = single_x.CrMasSupPostRegionsLatitude;
                        one_region.CrMasSupPostRegionsLongitude = single_x.CrMasSupPostRegionsLongitude;
                        one_region.CrMasSupPostRegionsStatus = single_x.CrMasSupPostRegionsStatus;
                        one_region.CrMasSupPostRegionsReasons = single_x.CrMasSupPostRegionsReasons;
                        _unitOfWork.CrMasSupPostRegion.Update(one_region);
                    }
                }

                foreach (var single in postRegion)
                {
                    var one_region = postRegion_x.Find(x => x.CrMasSupPostRegionsCode == single.CrMasSupPostRegionsCode);
                    if (one_region == null)
                    {
                        // //var newOne = mapper.Map<CrMasSupPostRegion>(single);
                        //CrMasSupPostRegion newOne = new CrMasSupPostRegion();
                        //newOne.CrMasSupPostRegionsCode = one_region.CrMasSupPostRegionsCode;
                        //newOne.CrMasSupPostRegionsArName = one_region.CrMasSupPostRegionsArName;
                        //newOne.CrMasSupPostRegionsEnName = one_region.CrMasSupPostRegionsEnName;
                        //newOne.CrMasSupPostRegionsLocation = one_region.CrMasSupPostRegionsLocation;
                        //newOne.CrMasSupPostRegionsLatitude = one_region.CrMasSupPostRegionsLatitude;
                        //newOne.CrMasSupPostRegionsLongitude = one_region.CrMasSupPostRegionsLongitude;
                        //newOne.CrMasSupPostRegionsStatus = one_region.CrMasSupPostRegionsStatus;
                        //newOne.CrMasSupPostRegionsReasons = one_region.CrMasSupPostRegionsReasons;
                        newList_remove.Add(single);
                    }
                }

                foreach(var one1 in newList_add)
                {
                    _unitOfWork.CrMasSupPostRegion.Add(one1);
                }

                foreach (var one2 in newList_remove)
                {
                    _unitOfWork.CrMasSupPostRegion.Delete(one2);
                }
                _unitOfWork.Complete();

                var postRegion_new = _unitOfWork.CrMasSupPostRegion.GetAll().ToList();

                var renamedList = postRegion_new.Select(one => new
                {
                    serial = postRegion_new.IndexOf(one) + 1,
                    code = one.CrMasSupPostRegionsCode,
                    arName = one.CrMasSupPostRegionsArName,
                    enName = one.CrMasSupPostRegionsEnName,
                    location = one.CrMasSupPostRegionsLocation,
                    longitude = one.CrMasSupPostRegionsLongitude,
                    latitude = one.CrMasSupPostRegionsLatitude,
                    status = one.CrMasSupPostRegionsStatus,
                    reasons = one.CrMasSupPostRegionsReasons,
                }).ToList();



                var result = new
                {
                    code = 1,
                    postRegion_old = renamedList,
                    //postRegion_updated = renamedList,
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                var result1 = new
                {
                    code = 0,
                };
                return Json(result1);

            }
        }



        [HttpGet]
        public async Task<IActionResult> get_Receipt_Totable_Action(string id)
        {
            try
            {
                var company_recipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceipt_LessorCode == id).ToList();

                var renamedList = company_recipts.Select(recipt => new
                {
                    serial = company_recipts.IndexOf(recipt) + 1,
                    Receipt_No = recipt.CrCasAccountReceipt_No,
                    DateTime = recipt.CrCasAccountReceipt_DateTime?.ToString("HH:mm:ss dd-MM-yyyy"),
                    Reference_No = recipt.CrCasAccountReceipt_Reference_No,
                    Debit = recipt.CrCasAccountReceipt_Payment,
                    Credit = recipt.CrCasAccountReceipt_Receipt,
                }).ToList();


                var result = new
                {
                    code = 1,
                    renamedList = renamedList,
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                var result1 = new
                {
                    code = 0,
                };
                return Json(result1);

            }
        }



        [HttpGet]
        public async Task<IActionResult> createExcel_saveAs_Receipt_Action(string id)
        {
            try
            {
                var company_recipts = _unitOfWork.CrCasAccountReceipt.FindAll(x => x.CrCasAccountReceipt_LessorCode == id).ToList();

                var renamedList = company_recipts.Select(recipt => new
                {
                    serial = company_recipts.IndexOf(recipt) + 1,
                    Receipt_No = recipt.CrCasAccountReceipt_No,
                    DateTime = recipt.CrCasAccountReceipt_DateTime?.ToString("dd-MM-yyyy HH:mm:ss"),
                    Reference_No = recipt.CrCasAccountReceipt_Reference_No,
                    Debit = recipt.CrCasAccountReceipt_Payment,
                    Credit = recipt.CrCasAccountReceipt_Receipt,
                }).ToList();


                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                //// html مسار الملف
                string filePath_for_src = "./Excels/DailyReport" + id.ToString() + datetime + ".xlsx";


                // مسار الملف الأصلي old excel
                string originalFilePath = "./wwwroot/Excels/DailyReportFormula.xlsx";

                // مسار النسخة الجديدة
                string newFilePath = "./wwwroot/Excels/DailyReport" + id.ToString() + datetime + ".xlsx";

                // فتح الملف الأصلي
                using (var workbook = new XLWorkbook(originalFilePath))
                {
                    // الحصول على الورقة المطلوبة (أو إضافة ورقة جديدة إذا كانت غير موجودة)
                    //var worksheet = workbook.Worksheets.Worksheet("Sheet1");
                    var worksheet = workbook.Worksheets.Worksheet("Receipt Daily Report");

                    // أو إنشاء ورقة جديدة
                    if (worksheet == null)
                    {
                        //worksheet = workbook.AddWorksheet("Sheet1");
                        worksheet = workbook.AddWorksheet("Receipt Daily Report");
                    }

                    // كتابة أسماء الأعمدة في الصف الأول
                    worksheet.Cell("B10").Value = "serial";
                    worksheet.Cell("C10").Value = "Receipt_No";
                    //worksheet.Cell("D10").Value = "DateTime";
                    //worksheet.Cell("E10").Value = "Reference_No";
                    worksheet.Cell("F10").Value = "Debit";
                    worksheet.Cell("G10").Value = "Credit";

                    //// ضبط عرض الأعمدة
                    //worksheet.Column("A").Width = 10; // عرض العمود A
                    //worksheet.Column("B").Width = 10; // عرض العمود B
                    //worksheet.Column("C").Width = 30; // عرض العمود C
                    //worksheet.Column("D").Width = 20;
                    //worksheet.Column("E").Width = 30;
                    //worksheet.Column("F").Width = 15;
                    //worksheet.Column("G").Width = 15;

                    // تعيين البيانات إلى الورقة بدءًا من B10
                    worksheet.Cell("B11").InsertData(renamedList);

                    // ضبط محاذاة النص ليكون في المنتصف (أفقيًا وعموديًا) لأسماء الأعمدة
                    worksheet.Range("A1:A1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //worksheet.Range("A:G").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("B1:B1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("C1:C1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("D1:D1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("E1:E1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("F1:F1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("G1:G1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    //worksheet.Range("B10:G10").Style.Fill.BackgroundColor = XLColor.BabyBlue;
                    //worksheet.Range("B10:G10").Style.Font.FontColor = XLColor.Black;
                    worksheet.Range("B10:G10").Style.Font.Bold = true;

                    // حفظ التغييرات كملف جديد
                    workbook.SaveAs(newFilePath);
                }

                var result = new
                {
                    code = 1,
                    link = filePath_for_src,
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                var result1 = new
                {
                    code = 0,
                };
                return Json(result1);

            }
        }


        [HttpGet]
        public async Task<IActionResult> createExcel_Receipt_Action(string id)
        {
            try
            {
                var company_recipts = _unitOfWork.CrCasAccountReceipt.FindAll(x=>x.CrCasAccountReceipt_LessorCode == id).ToList();

                var renamedList = company_recipts.Select(recipt => new
                {
                    serial = company_recipts.IndexOf(recipt)+1,
                    Receipt_No = recipt.CrCasAccountReceipt_No,
                    DateTime = recipt.CrCasAccountReceipt_DateTime?.ToString("dd-MM-yyyy HH:mm:ss"),
                    Reference_No = recipt.CrCasAccountReceipt_Reference_No,
                    Debit = recipt.CrCasAccountReceipt_Payment,
                    Credit = recipt.CrCasAccountReceipt_Receipt,
                }).ToList();

                ////var items = Enumerable.Range(1, 7).Select(x => new
                ////{
                ////    Prop1 = $"Text #{x}",
                ////    Prop2 = x * 1000,
                ////    Prop3 = DateTime.Now.AddDays(-x),
                ////});
                ////var excel = items.ToExcel();

                ////var excel = renamedList.ToExcel();
                //////var excel = renamedList.ToExcel(schema => schema
                //////                        .SheetName("receipt Daily Report"));


                //var datetime = DateTime.Now.ToString("dd-MM-yyyy__hh.mm.tt");
                var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                //// مسار الملف
                ////string filePath = @"C:\path\to\your\file.txt";
                ////string filePath_for_src = "./Excels/result_" + id.ToString() + ".xlsx";
                string filePath_for_src = "./Excels/DailyReport" + id.ToString() + datetime + ".xlsx";

                //// كتابة البيانات إلى الملف
                //System.IO.File.WriteAllBytes("./wwwroot/Excels/DailyReport" + id.ToString() + datetime + ".xlsx", excel);

                ////File.WriteAllBytes("./result.xlsx", excel);


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.AddWorksheet("receipt Daily Report");

                    // كتابة أسماء الأعمدة في الصف الأول
                    worksheet.Cell("B10").Value = "serial";
                    worksheet.Cell("C10").Value = "Receipt_No";
                    worksheet.Cell("D10").Value = "DateTime";
                    worksheet.Cell("E10").Value = "Reference_No";
                    worksheet.Cell("F10").Value = "Debit";
                    worksheet.Cell("G10").Value = "Credit";

                    // ضبط عرض الأعمدة
                    worksheet.Column("A").Width = 10; // عرض العمود A
                    worksheet.Column("B").Width = 10; // عرض العمود B
                    worksheet.Column("C").Width = 30; // عرض العمود C
                    worksheet.Column("D").Width = 20;
                    worksheet.Column("E").Width = 30;
                    worksheet.Column("F").Width = 15;
                    worksheet.Column("G").Width = 15;

                    // تعيين البيانات إلى الورقة بدءًا من B10
                    worksheet.Cell("B11").InsertData(renamedList);

                    // ضبط محاذاة النص ليكون في المنتصف (أفقيًا وعموديًا) لأسماء الأعمدة
                    worksheet.Range("A1:A1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //worksheet.Range("A:G").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("B1:B1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("C1:C1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("D1:D1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("E1:E1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("F1:F1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Range("G1:G1000").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    worksheet.Range("B10:G10").Style.Fill.BackgroundColor = XLColor.BabyBlue;
                    worksheet.Range("B10:G10").Style.Font.FontColor = XLColor.Black;
                    worksheet.Range("B10:G10").Style.Font.Bold = true;

                    // حفظ الملف
                    workbook.SaveAs("./wwwroot/Excels/DailyReport" + id.ToString() + datetime + ".xlsx");
                }


                var result = new
                {
                    code = 1,
                    link = filePath_for_src,
                };
                return Json(result);
            }catch (Exception ex)
            {
                var result1 = new
                {
                    code = 0,
                };
                return Json(result1);

            }
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteMessage_Page()
        {
            var One_WhatsUp = _unitOfWork.Company.FindAll(x => x.companyStatus == "A").ToList();
            var connects = _unitOfWork.Connect.FindAll(x => x.connectStatus == "A").ToList();

            List<Company> List_filtered = new List<Company>();
            foreach (var connect in connects)
            {
                var One_setup = One_WhatsUp.Find(x => x.companyId == connect.connectId);
                if (One_setup != null)
                {
                    List_filtered.Add(One_setup);
                }
            }
            //var Account_ApiToken = _unitOfWork.AccountWhatsUp.Find(x=>x.AccountWhatsUpCompanyId=="0000")?.AccountWhatsUpApiToken;

            var Renters = _unitOfWork.Renter.GetAll().OrderBy(x => x.RenterId).ToList();
            //var messages = _unitOfWork.Message.GetAll().ToList();

            WhatsUpVM whatsUpVM = new WhatsUpVM();
            whatsUpVM.Company_List = List_filtered;
            whatsUpVM.Renter_List = Renters;
            //whatsUpVM.Message_List = messages;
            return View(whatsUpVM);
        }


        [HttpGet]
        public async Task<IActionResult> Get_AllRenter()
        {
            var renters = _unitOfWork.Renter.GetAll().OrderBy(x => x.RenterId).ToList();

            var result = new
            {
                renters = renters,
            };
            return Json(result);

        }

        [HttpGet]
        public async Task<IActionResult> Get_Connect_Api_Token_and_DeviceSerial(string connect_id)
        {
            var exist_Connect = _unitOfWork.Connect.Find(x=>x.connectId == connect_id);
            var renters = _unitOfWork.Renter.GetAll().OrderBy(x => x.RenterId).ToList();
            //var api_Token = exist_Connect?.ConnectApiToken ?? string.Empty;
            //var device_serial = exist_Connect?.ConnectDeviceSerial ?? string.Empty;
            var messageList = _unitOfWork.Message.FindAll(x => x.MessageConnectId == connect_id && x.MessageStatus != "5").OrderByDescending(x => x.Message_Sent_DateTime).ToList();
            List<string> messageStatus = new List<string>();

            foreach (var message in messageList)
            {
                if (message.MessageStatus=="2")
                {
                    messageStatus.Add("ارسلت");
                }
                else if (message.MessageStatus == "3")
                {
                    messageStatus.Add("استلمت");
                }
                else if (message.MessageStatus == "4")
                {
                    messageStatus.Add("قرأت");
                }
            }



            var result = new
            {
                //api_Token = api_Token,
                //device_serial = device_serial,
                renters = renters,
                messageList = messageList,
                messageStatus = messageStatus
            };
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get_Message_status_Action(string message_id)
        {
            //var device_serial = exist_Connect?.ConnectDeviceSerial ?? string.Empty;
            var message = _unitOfWork.Message.Find(x => x.MessageId == message_id && x.MessageStatus != "0");
            var messageStatus = "";

            if (message.MessageStatus == "0")
            {
                messageStatus = "حذفت";
            }
            else if (message.MessageStatus == "1")
            {
                messageStatus = "ارسلت";
            }
            else if (message.MessageStatus == "2")
            {
                messageStatus = "استلمت";
            }
            else if (message.MessageStatus == "3")
            {
                messageStatus = "قرأت";
            }



            var result = new
            {
                //api_Token = api_Token,
                //device_serial = device_serial,
                message = message,
                messageStatus = messageStatus,
                status = message.MessageStatus,
            };
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> save_Message_Action(string message_Id, string message_text,string type, string Renter_id, string connect_id, string phoneNumber)
        {
            if (message_Id!=""&& message_text!="" && type != "" && Renter_id != "" && connect_id != "" && phoneNumber != "")
            {
                Message message = new Message()
                {
                    MessageId = message_Id,
                    MessageRenterId = Renter_id,
                    MessageConnectId = connect_id,
                    Message_Sent_DateTime = DateTime.Now,
                    MessagePhoneNumberFull = phoneNumber,
                    MessageText = message_text,
                    MessageType = type,
                    MessageStatus = "1"
                };
                await _unitOfWork.Message.AddAsync(message);
                _unitOfWork.Complete();
                              
                var result1 = new
                {
                    code = 1
                };
                return Json(result1);
            }
            var result = new
            {
                code = 0
            };
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update_Message_status_Delete_Action(string message_Id, string connect_id)
        {
            if (message_Id != "" && connect_id != "" )
            {
                var exist_message = _unitOfWork.Message.FindAll(x => x.MessageId == message_Id && x.MessageConnectId == connect_id).FirstOrDefault();
                if (exist_message != null)
                {
                    exist_message.Message_Deleted_DateTime = DateTime.Now;
                    exist_message.MessageStatus = "5";

                    _unitOfWork.Message.Update(exist_message);
                    _unitOfWork.Complete();
                

                var result1 = new
                {
                    code = 1
                };
                return Json(result1);
            } }

            var result = new
            {
                code = 0
            };
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> Get_MessagesList_status_Action(string connect_id)
        {
            var existing_messages = _unitOfWork.Message.FindAll(x => x.MessageStatus != "5" && x.MessageStatus != "4" && x.MessageConnectId == connect_id).Select(x =>x.MessageId).ToList();
            if (existing_messages?.Count() > 0)
            {

                var result1 = new
                {
                    code = 1,
                    existing_messages = existing_messages
                };
                return Json(result1);

            }
            var result2 = new
            {
                code = 0
            };
            return Json(result2);
        }

            [HttpPut]
        public async Task<IActionResult> Update_Message_status_Action(List<MessageVM> messagesVM, string connect_id)
        {
            if (messagesVM.Count() > 0 && connect_id != "" )
            {
                messagesVM = messagesVM.OrderBy(x=>x.message_id).ToList();
                var existing_messages = _unitOfWork.Message.FindAll(x => x.MessageStatus != "5" && x.MessageStatus != "4" && x.MessageConnectId == connect_id).OrderBy(x=>x.MessageId).ToList();
                if (existing_messages?.Count() > 0 )
                {
                    foreach ( var single in messagesVM)
                    {
                        var exist = existing_messages.Where(x => x.MessageId == single.message_id).FirstOrDefault();
                        exist.MessageStatus = single.receipt;

                        if (single.receipt == "3")
                        {
                            if(exist.Message_Received_DateTime == null )
                            {
                                exist.Message_Received_DateTime = DateTime.Now;
                            }
                        }
                        else if(single.receipt == "4")
                        {
                            if (exist.Message_Received_DateTime == null)
                            {
                                exist.Message_Received_DateTime = DateTime.Now;
                            }
                            if (exist.Message_Read_DateTime == null)
                            {
                                exist.Message_Read_DateTime = DateTime.Now;
                            }

                        }
                        _unitOfWork.Message.Update(exist);
                    }

                    _unitOfWork.Complete();


                    var result1 = new
                    {
                        code = 1
                    };
                    return Json(result1);
                }
            }

            var result = new
            {
                code = 0
            };
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> save_ApiToken_Action(string connect_id, string api_Token)
        {
            var this_Connect = _unitOfWork.Connect.Find(x => x.connectId == connect_id);

            if (connect_id != "" && api_Token != "" && this_Connect != null)
            {
                //this_Connect.ConnectApiToken = api_Token;
                                
                _unitOfWork.Connect.Update(this_Connect);
                _unitOfWork.Complete();

                var result1 = new
                {
                    code = 1
                };
                return Json(result1);
            }
            var result = new
            {
                code = 0
            };
            return Json(result);
        }

    }
}
