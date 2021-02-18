using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Chat;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Hub;

namespace SimpleChat.Client.Components
{
    public partial class MessagesScroller : IDisposable
    {
        private const string UserIdKeyName = "UserId";
        private const string DefaultAvatar = "images/defaultAvatar.jpg";
        private const int DefaultMessagesTop = 20;
        
        private HubConnection hubConnection;
        private List<MessageContract> messages = new List<MessageContract>();
        
        private DotNetObjectReference<MessagesScroller> objRef;
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        
        [Inject]
        private IHttpClientService Http { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }
        
        [Parameter]
        public ChatContract Chat { get; set; }
        
        private string UserId { get; set; }

        [JSInvokable]
        public async Task GetHistoryMessages()
        {
            var historyMessages =
                await Http.GetAsync<IEnumerable<MessageContract>>(
                    $"api/messages?chatId={Chat.Id}&Skip={messages.Count}&Top={DefaultMessagesTop}");
            messages.InsertRange(0, historyMessages.Reverse());
            StateHasChanged();
        }
        
        protected override async Task OnInitializedAsync()
        {
            UserId = await LocalStorageService.GetStringAsync(UserIdKeyName);
        }
        
        protected override async Task OnParametersSetAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri(HubConstants.ChatUri))
                .Build();

            hubConnection.On<MessageContract>(HubConstants.ReceiveMessage, mes =>
            {
                if (mes.Chat.Id == Chat.Id)
                {
                    messages.Add(mes);
                    StateHasChanged();
                }
            });

            var hubConnectionTask = hubConnection.StartAsync();
            var loadChatTask = Http.GetAsync<ChatContract>($"api/chats/{Chat.Id}");

            await Task.WhenAll(hubConnectionTask.ContinueWith(_ => hubConnection.InvokeAsync(HubConstants.Enter, Chat.Id)), loadChatTask);

            messages = Chat.Messages.ToList();
            messages.Reverse();
            
            UserId = await LocalStorageService.GetStringAsync(UserIdKeyName);
            
            objRef = DotNetObjectReference.Create(this);
            await JsRuntime.InvokeAsync<string>("addPaginationEvent", objRef);
        }
        
        public void Dispose()
        {
            objRef?.Dispose();
            hubConnection.DisposeAsync();
        }
    }
}