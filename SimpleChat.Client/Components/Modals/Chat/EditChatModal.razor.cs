using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Resources.ResourceFiles;
using SimpleChat.Client.Services;
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
            OnClose.InvokeAsync();
        }

        private async Task LoadPhoto(InputFileChangeEventArgs e) => Chat.Photo = await LoadFileService.LoadFile(e);
    }
}