using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Components.Modals.Chat
{
    public partial class CreateChatModal
    {
        private bool isPublic;
        private string chatName;
        private byte[] photo;

        [Inject] 
        private IHttpClientService Http { get; set; }

        [Parameter] 
        public EventCallback OnClose { get; set; }


        private async Task CreateAsync()
        {
            var chat = new CreateChatContract
            {
                IsPublic = isPublic,
                Name = chatName,
                Photo = photo,
            };

            await Http.PostAsync($"api/chats/", chat);
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
                photo = ms.ToArray();
                StateHasChanged();
            }
            catch
            {
                throw;
            }
        }

        private string GetImg()
        {
            if (photo != null)
            {
                var base64 = Convert.ToBase64String(photo);
                return $"data:image/jpeg;base64,{base64}";
            }

            return null;
        }
    }
}