using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat.Shared.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Services;
using SimpleChat.Client.Shared;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Client.Pages
{
    public partial class ChatPage : IDisposable
    {
        private const string UserNameKeyName = "UserName";
        private const string UserImgKeyName = "UserImgUrl";
        private const string UserIdKeyName = "UserId";
        
        private HubConnection hubConnection;
        private List<MessageContract> messages = new List<MessageContract>();
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

        public void Dispose() => _ = hubConnection.DisposeAsync();
    }
}
