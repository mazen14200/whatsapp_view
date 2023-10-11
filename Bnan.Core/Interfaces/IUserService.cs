using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUserService
    {
        Task<CrMasUserInformation?> GetUserByUserNameAsync(string username);
        CrMasUserInformation? GetUserByUserName(ClaimsPrincipal user);
        Task<List<CrMasUserInformation?>> GetAllUsersByLessor(string lessorId);
        Task<CrMasUserInformation?> GetUserLessor(System.Security.Claims.ClaimsPrincipal user);
        bool CheckUserifAuth(ClaimsPrincipal user);
        Task UpdateAsync(CrMasUserInformation user);
        Task SaveChanges(CrMasUserInformation user);

    }
}