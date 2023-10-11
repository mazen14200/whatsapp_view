using Bnan.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure
{
    public class PressenceHub : Hub
    {
        private readonly IUserService _user; 

        public PressenceHub(IUserService user)
        {
            _user = user;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _user.GetUserByUserNameAsync(Context.User.Identity.Name);
            if(user != null)
            {
                user.CrMasUserInformationOperationStatus = true;
                await _user.SaveChanges(user);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user =  await _user.GetUserByUserNameAsync(Context.User.Identity.Name);
            if (user != null)
            {
                user.CrMasUserInformationOperationStatus = false;
                await _user.SaveChanges(user);

                await base.OnDisconnectedAsync(exception);
            }
        }

    }
}
