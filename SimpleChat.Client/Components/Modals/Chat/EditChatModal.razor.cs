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
        
        [Parameter] 
        public EventCallback OnClose { get; set; }

        [Parameter] 
        public ChatContract Chat { get; set; }


        private async Task EditAsync()
        {
            await Http.PutAsync($"api/chats/{Chat.Id}", Chat);
            OnClose.InvokeAsync();
        }

        private async Task LoadFile(InputFileChangeEventArgs e)
        {
            try
            {
                using var reader =
                    new StreamReader(e.File.OpenReadStream(AppConstants.FileConstants.MaxFileSize));
                var ms = new MemoryStream();
                await reader.BaseStream.CopyToAsync(ms);
                Chat.Photo = ms.ToArray();
            }
            catch
            {
                ErrorState.SetError(Resource.Error, Resource.LoadFileError);
                throw;
            }
        }
    }
}