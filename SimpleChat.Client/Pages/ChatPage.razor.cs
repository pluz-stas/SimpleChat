using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat.Shared.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Contracts.Chat;
using static System.String;


namespace SimpleChat.Client.Pages
{
    public partial class ChatPage : IDisposable
    {
        private const string UserNameKeyName = "UserName";
        private const string UserImgKeyName = "UserImgUrl";
        private const string UserIdKeyName = "UserId";
        private const string DefaultAvatar = "https://t4.ftcdn.net/jpg/03/46/93/61/360_F_346936114_RaxE6OQogebgAWTalE1myseY1Hbb5qPM.jpg";
        
        private HubConnection hubConnection;
        private List<MessageContract> messages = new List<MessageContract>();
        private List<List<MessageContract>> messagesGroups = new List<List<MessageContract>>();
        private string userInput;
        private string messageInput;
        private ChatContract chat;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IHttpClientService Http { get; set; }
        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        [Parameter]
        public int ChatId { get; set; }
        public string UserId { get; set; }

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        protected override async Task OnParametersSetAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri(HubConstants.ChatUri))
                .Build();

            hubConnection.On<MessageContract>(HubConstants.ReceiveMessage, mes =>
            {
                if (mes.Chat.Id == ChatId)
                {
                    messages.Add(mes);
                    StateHasChanged();
                }
            });

            var hubConnectionTask = hubConnection.StartAsync();
            var loadChatTask = Http.GetAsync<ChatContract>($"api/chats/{ChatId}");

            await Task.WhenAll(hubConnectionTask.ContinueWith(_ => hubConnection.InvokeAsync(HubConstants.Enter, ChatId)), loadChatTask);

            chat = loadChatTask.Result;

            messages = chat.Messages.ToList();
            messages.Reverse();
            
            UserId = await LocalStorageService.GetStringAsync(UserIdKeyName);
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

        private async Task SetMessageGroups()
        {
            List<MessageContract> messageGroup = null;
            foreach (var message in messages)
            {
                var userId = message.User.UserId;
                if (!IsNullOrEmpty(userId))
                {
                    if (messageGroup != null)
                    {
                        if (messageGroup.Last().User.UserId == userId)
                        {
                            messageGroup.Add(message);
                            continue;
                        }
                    }
                }
                messageGroup.Add(message);
                messageGroup.Clear();

            }
        }
        
        private async Task GetHistoryMessages()
        {
            var historyMessages = await Http.GetAsync<IEnumerable<MessageContract>>($"api/messages?chatId={ChatId}&Skip={messages.Count}&Top=5");
            messages.InsertRange(0, historyMessages.Reverse());
        }

        public void Dispose() => _ = hubConnection.DisposeAsync();
    }
}
