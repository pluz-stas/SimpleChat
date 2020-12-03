using Microsoft.AspNetCore.Components;
using SimpleChat.Shared.Contracts;

namespace SimpleChat.Client.Components
{
    public partial class ChatCard
    {
        [Parameter]
        public Chat Chat { get; set; }
    }
}
