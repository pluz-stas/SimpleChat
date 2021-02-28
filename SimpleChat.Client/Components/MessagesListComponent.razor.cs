using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Hub;

namespace SimpleChat.Client.Components
{
    public partial class MessagesListComponent : IDisposable
    {
        private const int DefaultMessagesTop = 20;

        private HubConnection hubConnection;
        private List<MessageContract> messages = new List<MessageContract>();
        private DotNetObjectReference<MessagesListComponent> objRef;

        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] private IHttpClientService Http { get; set; }

        [Inject] private IJSRuntime JsRuntime { get; set; }

        [Parameter] public int ChatId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri(HubConstants.ChatUri))
                .Build();

            hubConnection.On<MessageContract>(HubConstants.ReceiveMessage, mes =>
            {
                if (mes.Chat.Id == ChatId)
                {
                    messages.Insert(0, mes);
                    StateHasChanged();
                    ScrollToBottom();
                }
            });

            await hubConnection.StartAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await hubConnection.InvokeAsync(HubConstants.Enter, ChatId);
            messages = (await GetMessagesAsync(0)).ToList();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                objRef = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync("addPaginationEvent", objRef);
                await JsRuntime.InvokeVoidAsync("addScrollButtonEvent");
            }
        }

        [JSInvokable]
        public async Task UpdateMessagesHistoryAsync()
        {
            messages.AddRange(await GetMessagesAsync(messages.Count));
            StateHasChanged();
        }

        private bool IsDateSplit(int messageIndex)
        {
            var previousMessage = messageIndex + 1 < messages.Count ? messages[messageIndex + 1] : null;

            return previousMessage != null && previousMessage.CreatedDate.ToLocalTime().Day != messages[messageIndex].CreatedDate.ToLocalTime().Day;
        }

        private void ScrollToBottom()
        {
            JsRuntime.InvokeVoidAsync("scrollToBottom");
        }

        private async Task<IEnumerable<MessageContract>> GetMessagesAsync(int skip) =>
            await Http.GetAsync<IEnumerable<MessageContract>>(
                $"api/messages?chatId={ChatId}&Skip={skip}&Top={DefaultMessagesTop}");

        private bool ShouldDisplayAvatar(MessageContract message, int messageIndex) =>
            !(messageIndex > 0 && messages[messageIndex - 1].User.UserId == message.User.UserId && message.User.UserId != null);

        private bool ShouldDisplayName(MessageContract message, int messageIndex) =>
            !(messageIndex + 1 < messages.Count && messages[messageIndex + 1].User.UserId == message.User.UserId && message.User.UserId != null);
        
        public void Dispose()
        {
            objRef?.Dispose();
            hubConnection.DisposeAsync();
        }
    }
}