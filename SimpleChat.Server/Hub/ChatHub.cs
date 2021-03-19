using System.Threading.Tasks;

namespace SimpleChat.Server.Hub
{
    /// <summary>
    /// Base hub.
    /// </summary>
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        /// <summary>
        /// Subscribes user to chat.
        /// </summary>
        /// <param name="chatId">Chat id.</param>
        /// <returns></returns>
        public async Task Enter(int chatId) => await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());           
    }
}
