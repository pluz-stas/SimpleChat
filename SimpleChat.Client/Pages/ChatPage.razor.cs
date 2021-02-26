using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Pages
{
    public partial class ChatPage
    {
        private string messageInput;
        private ChatContract chat;

        [Inject]
        private IHttpClientService Http { get; set; }
        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        [Parameter]
        public int ChatId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            chat = await Http.GetAsync<ChatContract>($"api/chats/{ChatId}");
        }

        private async Task Send()
        {
            var user = new ShortUserInfoContract
            {
                UserName = await LocalStorageService.GetStringAsync(LocalStorageAttributes.UserNameKeyName),
                UserImg = await LocalStorageService.GetStringAsync(LocalStorageAttributes.UserImgKeyName),
                UserId = await LocalStorageService.GetStringAsync(LocalStorageAttributes.UserIdKeyName)
            };
                
            var message = new CreateMessageContract
            {
                Content = messageInput,
                User = user,
            };

            await Http.PostAsync($"api/messages/{ChatId}", message);

            messageInput = string.Empty;
        }
    }
}
