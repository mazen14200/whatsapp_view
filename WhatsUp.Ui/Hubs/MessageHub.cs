using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WhatsUp.Ui.Hubs
{
    public class MessageHub : Hub
    {
        public Task sendMessageToAll(string message) 
        { 
            return Clients.All.SendAsync("ReceiveMessage",message);
        }
    }
}
