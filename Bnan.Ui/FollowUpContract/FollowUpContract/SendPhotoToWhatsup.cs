using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Image = System.Drawing.Image;
using Bnan.Core.Models;

namespace FollowUpContract
{
    public class SendPhotoToWhatsup
    {
        public async static void SendMailBeforeOneDay(CrCasRenterContractBasic contract, string filePath, string number, string token)
        {
            string wwwrootPathOfOtherProject = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName).FullName;
            string imagePath = Path.Combine(wwwrootPathOfOtherProject, "wwwroot", filePath);
            string savedModified = Path.Combine(wwwrootPathOfOtherProject, "wwwroot", "images", "111.png");

            // Load the image
            Bitmap bitmap = new Bitmap(imagePath);
            Graphics graphics = Graphics.FromImage(bitmap);

            // Define text color
            Brush brush = new SolidBrush(Color.Black);

            // Define text font
            Font font = new Font("Arial", 35, FontStyle.Bold);
            Font fontContratNo = new Font("Arial", 27, FontStyle.Bold);

            // Text to display
            string arName = contract.CrCasRenterContractBasic4.CrCasRenterLessorNavigation.CrMasRenterInformationArName;
            string enName = contract.CrCasRenterContractBasic4.CrCasRenterLessorNavigation.CrMasRenterInformationEnName;
            string contractNo = contract.CrCasRenterContractBasicNo;

            // Define rectangle
            Rectangle arNameRec = new Rectangle(450, 435, 1000, 500);
            Rectangle enNameRec = new Rectangle(190, 700, 1000, 500);
            Rectangle ContractNoRec = new Rectangle(40, 220, 1000, 500);

            // Draw text on image
            graphics.DrawString(arName, font, brush, arNameRec);
            graphics.DrawString(enName, font, brush, enNameRec);
            graphics.DrawString(contractNo, fontContratNo, brush, ContractNoRec);

            // Save the output file
            bitmap.Save(savedModified);

            //Create HttpClient
            using (var client = new HttpClient())
            {
                // Create multipart form data
                var formData = new MultipartFormDataContent();

                // Add image file to the form data
                var fileStream = File.OpenRead(savedModified);
                formData.Add(new StreamContent(fileStream), "file", Path.GetFileName(@savedModified));
                formData.Add(new StringContent(" "), "message");

                // Add number to the form data
                formData.Add(new StringContent(number), "number");

                // Make the API request to send the image
                var url = $"https://business.enjazatik.com/api/v1/send-media?token={token}";
                var response = await client.PostAsync(url, formData);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody); // Output the response
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            File.Delete(savedModified);
        }
    }
}
