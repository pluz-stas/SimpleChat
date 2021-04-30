using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using SimpleChat.Client.Services;
using SimpleChat.Client.Services.Interfaces;
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
        
        [Inject]
        private LoadFileService LoadFileService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        private async Task CreateAsync()
        {
            var chat = new CreateChatContract
            {
                IsPublic = isPublic,
                Name = chatName,
                Photo = photo,
                InviteLink = isPublic ? null : Guid.NewGuid().ToString()
            };
            
            var responseMessage = await Http.PostAsync($"api/chats/", chat);
            var chatContract = JsonConvert.DeserializeObject<ChatContract>(await responseMessage.Content.ReadAsStringAsync());
            await OnClose.InvokeAsync();
            NavigationManager.NavigateTo(
                !string.IsNullOrEmpty(chatContract.InviteLink)
                    ? $"chat/getByLink/{chatContract.InviteLink}"
                    : $"chat/{chatContract.Id}", true);
        }

        private async Task LoadPhoto(InputFileChangeEventArgs e) => photo = await LoadFileService.LoadFile(e);
    }
}