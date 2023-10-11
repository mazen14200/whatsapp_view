using Bnan.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.MiddleWare
{
    public class CheckCultureOfUser
    {
        private readonly RequestDelegate _next;




        public CheckCultureOfUser(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {

            var user = await userService.GetUserByUserNameAsync(context.User.Identity.Name);
            string? defaultCulture = null;
            if (user != null)
            {
                if (user.CrMasUserInformationDefaultLanguage == "AR")
                {
                    context.Response.Cookies.Append(
                     CookieRequestCultureProvider.DefaultCookieName,
                     CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("ar-EG")),
                     new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
             );
                }
            }



            // Call the next middleware in the pipeline
            await _next(context);
        }


    }

}


