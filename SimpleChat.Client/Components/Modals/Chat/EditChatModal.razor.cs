using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Components.Modals.Chat
{
    public partial class EditChatModal
    {
        [Inject] 
        private IHttpClientService Http { get; set; }

        [Parameter] 
        public EventCallback OnClose { get; set; }
        
        [Parameter] 
        public ChatContract Chat { get; set; }


        private async Task EditAsync()
        {
            await Http.PutAsync($"api/chats/{Chat.Id}", Chat);
            OnClose.InvokeAsync();
        }

        private long maxFileSize = 1024 * 1024;
        async Task LoadFile(InputFileChangeEventArgs e)
        {
            try
            {
                using var reader =
                    new StreamReader(e.File.OpenReadStream(maxFileSize));
                var ms = new MemoryStream();
                await reader.BaseStream.CopyToAsync(ms);
                Chat.Photo = ms.ToArray();
                StateHasChanged();
            }
            catch
            {
                throw;
            }
        }

        private string GetImg()
        {
            if (Chat.Photo != null)
            {
                var base64 = Convert.ToBase64String(Chat.Photo);
                return $"data:image/jpeg;base64,{base64}";
            }

            return null;
        }
    }
}