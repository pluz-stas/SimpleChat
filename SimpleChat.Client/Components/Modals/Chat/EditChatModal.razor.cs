using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SimpleChat.Client.Services;
using SimpleChat.Client.Services.Interfaces;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Components.Modals.Chat
{
    public partial class EditChatModal
    {
        [Inject] 
        private IHttpClientService Http { get; set; }
        
        [Inject]
        private ErrorStateService ErrorState { get; set; }
        
        [Inject]
        private LoadFileService LoadFileService { get; set; }
        
        [Parameter] 
        public EventCallback OnClose { get; set; }

        [Parameter] 
        public ChatContract Chat { get; set; }

        
        private async Task EditAsync()
        {
            await Http.PutAsync($"api/chats/{Chat.Id}", Chat);
            await OnClose.InvokeAsync();
        }

        private async Task LoadPhoto(InputFileChangeEventArgs e) => Chat.Photo = await LoadFileService.LoadFile(e);
    }
}