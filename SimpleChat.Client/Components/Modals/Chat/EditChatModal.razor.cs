using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SimpleChat.Client.Resources.ResourceFiles;
using SimpleChat.Client.Services;
using SimpleChat.Client.Services.Interfaces;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Components.Modals.Chat
{
    public partial class EditChatModal
    {
        private bool isMasterPasswordNewState;
        private bool isChangeMasterPassword;
        private string passwordResponse;
        
        [Inject] 
        private IHttpClientService Http { get; set; }
        
        [Inject]
        private ErrorStateService ErrorState { get; set; }
        
        [Inject]
        private LoadFileService LoadFileService { get; set; }
        
        [Parameter] 
        public EventCallback OnClose { get; set; }

        [Parameter] 
        public EditChatContract Chat { get; set; }

        
        private async Task EditAsync()
        {
            
            Chat.IsMasterPassword = isMasterPasswordNewState;
            var response = await Http.PutAsync($"api/chats/{Chat.Id}", Chat);

            if (response.IsSuccessStatusCode)
            {
                await OnClose.InvokeAsync();
            }
            else
            {
                passwordResponse = await response.Content.ReadAsStringAsync();
            }
        }

        private async Task LoadPhoto(InputFileChangeEventArgs e) => Chat.Photo = await LoadFileService.LoadFile(e);
    }
}