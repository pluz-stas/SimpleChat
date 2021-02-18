using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat.Shared.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Contracts.Chat;
using static System.String;


namespace SimpleChat.Client.Pages
{
    public partial class ChatPage
    {
        private const string UserNameKeyName = "UserName";
        private const string UserImgKeyName = "UserImgUrl";
        private const string UserIdKeyName = "UserId";
        
        private List<MessageContract> messages = new List<MessageContract>();
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
                UserName = await LocalStorageService.GetStringAsync(UserNameKeyName),
                UserImg = await LocalStorageService.GetStringAsync(UserImgKeyName),
                UserId = await LocalStorageService.GetStringAsync(UserIdKeyName)
            };
                
            var message = new CreateMessageContract
            {
                Content = messageInput,
                User = user,
            };

            await Http.PostAsync($"api/messages/{ChatId}", message);

            messageInput = string.Empty;
        }
        
        private async Task GetHistoryMessages()
        {
            var historyMessages = await Http.GetAsync<IEnumerable<MessageContract>>($"api/messages?chatId={ChatId}&Skip={messages.Count}&Top=5");
            messages.InsertRange(0, historyMessages.Reverse());
        }
    }
}
