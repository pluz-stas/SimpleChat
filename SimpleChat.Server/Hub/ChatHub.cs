using Microsoft.AspNetCore.SignalR;
using SimpleChat.Shared.Hub;
using System.Threading.Tasks;

namespace SimpleChat.Server.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync(HubConstants.ReceiveMessage, user, message);
        }
    }
}
