using Bnan.Core.Models;
using FollowUpContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Policy;
using System.Timers;
namespace BnanFollowUpContracts
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args)
        {
            await UpdateDatabase();
            //await UpdateLoginUser();
            //await SendImageAsync("201090504001", "C:\\Users\\WilzY\\Desktop\\Work\\BnanJordan\\Bnan.Ui\\wwwroot\\images\\common\\Contract\\ContractCard.png", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJudW1iZXIiOiJKYXNlcjExIiwic2VyaWFsIjoiMTk5ZmUzYjFlYjc2MjNlIiwiaWF0IjoxNzA3NzMxNjI4LCJleHAiOjE3OTQxMzE2Mjh9.O_4RW4vYAays1ZL7D-OlOQh6C5P5xVYrT3pZ2Oi9Yak");

        }
        public static async Task UpdateDatabase()
        {
            //Console.WriteLine($"Updating the database at: {DateTime.Now}");
            BnanKSAContext database = new BnanKSAContext();
            var contracts = database.CrCasRenterContractAlerts.Where(d => d.CrCasRenterContractAlertContractStatus == "A").ToList();
            foreach (var item in contracts)
            {
                var IsTrue = true;
                string? path = "";
                //check if contract will end after 24 hours
                var contract = database.CrCasRenterContractBasics.Include(x => x.CrCasRenterContractBasic4).ThenInclude(x => x.CrCasRenterLessorNavigation).FirstOrDefault(x => x.CrCasRenterContractBasicNo == item.CrCasRenterContractAlertNo);
                var user = database.CrMasUserInformations.Include(x => x.CrMasUserInformationLessorNavigation).ThenInclude(x => x.CrMasLessorImage).FirstOrDefault(d => d.CrMasUserInformationCode == contract.CrCasRenterContractBasicUserInsert);
                if (item.CrCasRenterContractAlertDayDate <= DateTime.Now && item.CrCasRenterContractAlertStatus == "0" && item.CrCasRenterContractAlertDays > 2)
                {
                    item.CrCasRenterContractAlertStatus = "1";
                    item.CrCasRenterContractAlertContractActiviteStatus = "1";
                    item.CrCasRenterContractAlertStatusMsg = "العقد ينتهي غدا";
                    path = user.CrMasUserInformationLessorNavigation.CrMasLessorImage.CrMasLessorImageTomorrowContractWhatUp;
                    database.SaveChanges();
                    IsTrue = false;
                }
                // check if contract will end after 4 hours
                if ((item.CrCasRenterContractAlertHourDate <= DateTime.Now && (item.CrCasRenterContractAlertStatus == "1" || item.CrCasRenterContractAlertStatus == "0")))
                {
                    item.CrCasRenterContractAlertStatus = "2";
                    item.CrCasRenterContractAlertContractActiviteStatus = "0";
                    item.CrCasRenterContractAlertStatusMsg = "العقد ينتهي بعد 4 ساعات";
                    path = user.CrMasUserInformationLessorNavigation.CrMasLessorImage.CrMasLessorImageHourContractWhatUp;
                    database.SaveChanges();
                    IsTrue = false;
                }
                // check if contract will end Now
                if (item.CrCasRenterContractAlertEndDate <= DateTime.Now && item.CrCasRenterContractAlertStatus == "2")
                {
                    item.CrCasRenterContractAlertStatus = "3";
                    item.CrCasRenterContractAlertStatusMsg = "العقد منتهي";
                    item.CrCasRenterContractAlertContractStatus = "E";
                    path = user.CrMasUserInformationLessorNavigation.CrMasLessorImage.CrMasLessorImageEndContractWhatUp;
                    database.CrCasRenterContractBasics.OrderByDescending(x => x.CrCasRenterContractBasicCopy).FirstOrDefault(d => d.CrCasRenterContractBasicNo == item.CrCasRenterContractAlertNo).CrCasRenterContractBasicStatus = "E";
                    database.SaveChanges();
                    IsTrue = false;
                }
                var message = item.CrCasRenterContractAlertStatusMsg + " - " + item.CrCasRenterContractAlertNo + " - " + DateTime.Now;
                var imagePath = path.Replace("~", "").TrimStart('/');
                string fromNumber = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJudW1iZXIiOiJKYXNlcjExIiwic2VyaWFsIjoiMTk5ZmUzYjFlYjc2MjNlIiwiaWF0IjoxNzA3NzMxNjI4LCJleHAiOjE3OTQxMzE2Mjh9.O_4RW4vYAays1ZL7D-OlOQh6C5P5xVYrT3pZ2Oi9Yak";
                string toNumber = user.CrMasUserInformationCallingKey + user.CrMasUserInformationMobileNo;

                if (!IsTrue)
                {
                    try
                    {
                        SendPhotoToWhatsup.SendMailBeforeOneDay(contract, imagePath,toNumber,fromNumber);

                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }

            }
            Console.WriteLine($"Finish update database at: {DateTime.Now}");
            System.Threading.Thread.Sleep(1000);
        }
        public static async Task UpdateLoginUser()
        {
            BnanKSAContext database = new BnanKSAContext();
            var users = database.CrMasUserInformations.Where(d => d.CrMasUserInformationStatus != "D" && d.CrMasUserInformationOperationStatus == true).ToList();
            foreach (var user in users)
            {
                var timeToday = DateTime.Now;
                var exitTimer = (double)(user.CrMasUserInformationExitTimer + 2);
                if (user.CrMasUserInformationLastActionDate?.AddMinutes(exitTimer) <= timeToday)
                {
                    user.CrMasUserInformationOperationStatus = false;
                    user.CrMasUserInformationExitLastDate = DateTime.Now;
                }
                database.SaveChanges();
            }
        }
    }
}
