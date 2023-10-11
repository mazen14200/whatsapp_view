using Bnan.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(CrMasUserInformation model);
        Task<bool> RegisterForCasAsync(CrMasUserInformation model);
        Task<bool> AddUserDefault(string LessorCode);
        Task<bool> AddRoleAsync(CrMasUserInformation user, string Role);
        Task<SignInResult> LoginAsync(string username, string password);
        Task<bool> CheckPassword(string username, string password);
        Task UserLogins(string username);
        Task SignOut();

    }
}