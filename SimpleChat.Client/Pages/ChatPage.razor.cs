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

        private async Task SendAsync()
        {
            var userNameTask = LocalStorageService.GetStringAsync(LocalStorageAttributes.UserNameKeyName);
            var userImgTask = LocalStorageService.GetStringAsync(LocalStorageAttributes.UserImgKeyName);
            var userIdTask = LocalStorageService.GetStringAsync(LocalStorageAttributes.UserIdKeyName);

            await Task.WhenAll(userNameTask, userIdTask, userImgTask);
            
            var user = new ShortUserInfoContract
            {
                UserId = userIdTask.Result,
                UserName = userNameTask.Result,
                UserImg = userImgTask.Result,
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
