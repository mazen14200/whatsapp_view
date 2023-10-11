﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Extensions
{
    public static class FileExtensions
    {
        
        public static async Task<string> SaveImageAsync(this IFormFile file,IWebHostEnvironment webHostEnvironment, string folderName, string fileName,string extension)
        {

            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, folderName);
            string filePath = Path.Combine(uploadsFolder, fileName+extension);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            string virtualPath = filePath.Replace(webHostEnvironment.WebRootPath, "~").Replace("\\", "/");
            return virtualPath;
        }

        public static async Task<bool?> RemoveImage(this IWebHostEnvironment webHostEnvironment, string folderName, string fileName,string extension)
        {
            string imagePath = Path.Combine(webHostEnvironment.WebRootPath, folderName, fileName+extension);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath); 
                return true;
            }
            return false;
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
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches", "100", "Receipt"),
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
        public static async Task<bool?> CreateFolderBranch(IWebHostEnvironment webHostEnvironment, string lessorCode,string branchCode)
        {
            var directoryPaths = new List<string>
                    {
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "Arabic Contract"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "English Contract"),
                        GetDirectoryPath(webHostEnvironment.WebRootPath,"images","Company",lessorCode, "Branches",branchCode, "Receipt"),
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
