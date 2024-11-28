using Azure;
using WhatsUp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Inferastructure.Extensions
{
    public static class TitleExtension
    {
        public static async Task SetPageTitleAsync(this ViewDataDictionary viewData, string system, string task, string subtask,string operationAr,string operationEn, string userName)    
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            var titleParts = new List<string>();

            if (!string.IsNullOrEmpty(system))
            {
                titleParts.Add(system);
            }
            if (!string.IsNullOrEmpty(task))
            {
                titleParts.Add(task);
            }
            if (!string.IsNullOrEmpty(subtask))
            {
                titleParts.Add(subtask);
            }
            if (currentCulture == "en-US")
            {
                if (!string.IsNullOrEmpty(operationEn))
                {
                    titleParts.Add(operationEn);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(operationAr))
                {
                    titleParts.Add(operationAr);
                }
            }
            
            if (!string.IsNullOrEmpty(userName))
            {
                titleParts.Add(userName);
            }
            viewData["Title"] = string.Join(" - ", titleParts);
        }
    }
}
