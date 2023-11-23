using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Bnan.Inferastructure.Repository
{
    public class UserService : IUserService
    {
        private readonly UserManager<CrMasUserInformation> _userManager;
        private readonly SignInManager<CrMasUserInformation> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(UserManager<CrMasUserInformation> userManager, SignInManager<CrMasUserInformation> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public  bool CheckUserifAuth(ClaimsPrincipal user)
        {
            return  _signInManager.IsSignedIn(user);
        }

        public  async Task<List<CrMasUserInformation?>> GetAllUsersByLessor(string lessorId)
        {

            var users=  await _userManager.Users.Where(x=>x.CrMasUserInformationLessor==lessorId).Include(x=>x.CrMasUserBranchValidities).ToListAsync();
            return users;
        }

        public Task<CrMasLessorInformation> GetLessorByUser(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public CrMasUserInformation? GetUserByUserName(ClaimsPrincipal user)
        {
            var crMasUserInformationCode = _userManager.GetUserId(user);

            var userLessor = _userManager.Users.Include(l => l.CrMasUserInformationLessorNavigation).FirstOrDefault(u => u.CrMasUserInformationCode == crMasUserInformationCode);
            return userLessor;
        }

       
        public async Task<CrMasUserInformation?> GetUserByUserNameAsync(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(l => l.CrMasUserInformationCode == username);
            
            return user;
        }

        public async Task<CrMasUserInformation?> GetUserLessor(ClaimsPrincipal user)
        {
            var crMasUserInformationCode = _userManager.GetUserId(user);

            var userLessor = await _userManager.Users.Include(l=>l.CrMasUserInformationLessorNavigation).FirstOrDefaultAsync(u => u.CrMasUserInformationCode == crMasUserInformationCode);
            return userLessor;
        }

        public async Task SaveChanges(CrMasUserInformation user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateAsync(CrMasUserInformation user)
        {
            await _userManager.UpdateSecurityStampAsync(user);
        }
    }
}