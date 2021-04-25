using Microsoft.AspNetCore.Components;
using SimpleChat.Shared.Contracts.Chat;
using System.Threading.Tasks;

namespace SimpleChat.Client.Components
{
    public partial class ChatCard
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public ChatContract Chat { get; set; }

        [Parameter]
        public bool IsSelected { get; set; }

        private void SelectChat() => NavigationManager.NavigateTo($"chat/{Chat.Id}");
    }
}
