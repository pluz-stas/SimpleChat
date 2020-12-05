using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat.Shared.Contracts;
using SimpleChat.Shared.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SimpleChat.Client.Pages
{
    public partial class ChatPage : IDisposable
    {
        private HubConnection hubConnection;
        private List<Message> messages = new List<Message>();
        private string userInput;
        private string messageInput;
        private Chat chat;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        [Parameter]
        public int ChatId { get; set; }

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        protected override async Task OnParametersSetAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri(HubConstants.ChatUri))
                .Build();

            hubConnection.On<Message>(HubConstants.ReceiveMessage, mes =>
            {
                if (mes.Chat.Id == ChatId)
                {
                    messages.Add(mes);
                    StateHasChanged();
                }
            });

            var hubConnectionTask = hubConnection.StartAsync();
            var loadChatTask = Http.GetFromJsonAsync<Chat>($"api/chats/{ChatId}");

            await Task.WhenAll(hubConnectionTask.ContinueWith(_ => hubConnection.InvokeAsync(HubConstants.Enter, ChatId)), loadChatTask);

            chat = loadChatTask.Result;

            messages = chat.Messages.ToList();
        }

        private async Task Send()
        {
            var message = new Message
            {
                Chat = chat,
                Content = messageInput,
                UserName = userInput,
                IsRead = false,
                CreatedDate = DateTime.Now.ToUniversalTime()
            };

            await Http.PostAsJsonAsync($"api/messages/", message);

            messageInput = string.Empty;
        }

        public void Dispose() => _ = hubConnection.DisposeAsync();
    }
}
