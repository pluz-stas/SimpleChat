using Microsoft.AspNetCore.Components;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Components
{
    public partial class ChatCard
    {
        [Parameter]
        public ChatContract Chat { get; set; }

        [Parameter]
        public EventCallback OnChatSelecting { get; set; }
    }
}
