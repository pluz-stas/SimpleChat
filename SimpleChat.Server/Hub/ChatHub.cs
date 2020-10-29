using Microsoft.AspNetCore.SignalR;
using SimpleChat.Shared.Hub;
using System.Threading.Tasks;

namespace SimpleChat.Server.Hub
{
    /// <summary>
    /// Base hub.
    /// </summary>
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        /// <summary>
        /// Sends message to group.
        /// </summary>
        /// <param name="user">Message author.</param>
        /// <param name="message">Message body.</param>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync(HubConstants.ReceiveMessage, user, message);
        }
    }
}
