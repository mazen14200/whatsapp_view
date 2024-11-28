﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Inferastructure.Extensions
{
    public static class FileExtensions
    {

        public static async Task<string> SaveImageAsync(this IFormFile file, IWebHostEnvironment webHostEnvironment, string folderName, string fileName, string extension)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, folderName);
            string filePath = Path.Combine(uploadsFolder, fileName + extension);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Check if the file exists and delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string virtualPath = filePath.Replace(webHostEnvironment.WebRootPath, "~").Replace("\\", "/");
            return virtualPath;
        }
        public static async Task<string> SaveImageAsync(this IFormFile file, IWebHostEnvironment webHostEnvironment, string folderName, string fileName, string extension,string oldPath)
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, folderName);
            string filePath = Path.Combine(uploadsFolder, fileName + extension);
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (!string.IsNullOrEmpty(oldPath))
            {
                string oldPathPysical = MapPath(webHostEnvironment, oldPath);
                // Check if the file exists and delete it
                if (File.Exists(oldPathPysical))
                {
                    File.Delete(oldPathPysical);
                }
            }
            

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string virtualPath = filePath.Replace(webHostEnvironment.WebRootPath, "~").Replace("\\", "/");
            return virtualPath;
        }
        public static async Task<bool?> RemoveImage(this IWebHostEnvironment webHostEnvironment, string folderName, string fileName, string extension)
        {
            string imagePath = Path.Combine(webHostEnvironment.WebRootPath, folderName, fileName + extension);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return true;
            }
            return false;
        }
        public static async Task<string?> SavePdf(this IWebHostEnvironment webHostEnvironment, string pdfFile, string lessor,string branch,string AccountNo,string language,string Type)
        {
            byte[] pdfBytes = Convert.FromBase64String(pdfFile.Split(",")[1]); // Split to remove the prefix "data:application/pdf;base64,"
            string wwwrootPath = webHostEnvironment.WebRootPath;
            string Pathh = "";
            if (Type=="Receipt")
            {
                if (language == "ar") Pathh = Path.Combine("images", "Company", lessor, "Branches", branch, "Arabic Receipt", AccountNo + ".pdf");
                else Pathh = Path.Combine("images", "Company", lessor, "Branches", branch, "English Receipt", AccountNo + ".pdf");
            }
            else
            {
                if (language == "ar") Pathh = Path.Combine("images", "Company", lessor, "Branches", branch, "Arabic Invoice", AccountNo + ".pdf");
                else Pathh = Path.Combine("images", "Company", lessor, "Branches", branch, "English Invoice", AccountNo + ".pdf");
            }
            
            string filePath = Path.Combine(wwwrootPath, Pathh);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            // Write the byte array to a file
            try
            {
                await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);
                // Set isTrue to true if the file write operation is successful
                bool isTrue = true;

                if (isTrue)
                {
                    string virtualPath1 = Pathh.Replace(webHostEnvironment.WebRootPath, "~").Replace("\\", "/");
                    string virtualPath = $"~/{virtualPath1}";
                    return virtualPath;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here if needed
                // For now, let's throw it again
                throw;
            }

            // If the file write operation was unsuccessful, return null
            return null;
        }
        public static async Task<bool?> RemoveImage(this IWebHostEnvironment webHostEnvironment, string path)
        {
            var pathA = MapPath(webHostEnvironment, path);
            string imagePath = Path.Combine(webHostEnvironment.WebRootPath, pathA);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return true;
            }
            return false;
        }

        public static string MapPath(this IWebHostEnvironment webHostEnvironment, string virtualPath)
        {
            // Remove the leading "~/" if present
            virtualPath = virtualPath.TrimStart('~', '/');

            // Combine the web root path with the virtual path
            string fullPath = Path.Combine(webHostEnvironment.WebRootPath, virtualPath.Replace('/', Path.DirectorySeparatorChar));

            return fullPath;
        }
        public static async Task<bool?> CreateFolderLessor(IWebHostEnvironment webHostEnvironment, string lessorCode)
        {
            var directoryPaths = new List<string>
                    {
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Support Images"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Contract Company"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Users"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Users","CAS"+lessorCode),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "Arabic Contract"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "English Contract"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "Arabic Receipt"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "English Receipt"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "Arabic Invoice"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "English Invoice"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "Documentions")
                    };

            foreach (var path in directoryPaths)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            return true;
        }
        public static async Task<bool?> CreateFolderBranch(IWebHostEnvironment webHostEnvironment, string lessorCode, string branchCode)
        {
            var directoryPaths = new List<string>
                    {
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "Arabic Contract"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "English Contract"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "Arabic Receipt"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "English Receipt"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "Arabic Invoice"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "English Invoice"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "Documentions")
                    };

            foreach (var path in directoryPaths)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            return true;
        }






        private static string GetDirectoryPath(params string[] parts)
        {
            return Path.Combine(parts);
        }

    }
}
