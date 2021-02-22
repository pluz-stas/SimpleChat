using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Message;
using SimpleChat.Shared.Hub;

namespace SimpleChat.Client.Components
{
    public partial class MessagesScroller : IDisposable
    {
        private const string UserIdKeyName = "UserId";
        private const string DefaultAvatar = "images/defaultAvatar.jpg";
        private const int DefaultMessagesTop = 20;

        private string userId;
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
        public int ChatId { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            userId = await LocalStorageService.GetStringAsync(UserIdKeyName);

            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri(HubConstants.ChatUri))
                .Build();

            hubConnection.On<MessageContract>(HubConstants.ReceiveMessage, mes =>
            {
                if (mes.Chat.Id == ChatId)
                {
                    messages.Insert(0, mes);
                    StateHasChanged();
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
            }
        }

        [JSInvokable]
        public async Task UpdateMessagesHistoryAsync()
        {
            messages.AddRange(await GetMessagesAsync(messages.Count));
            StateHasChanged();
        }

        public void Dispose()
        {
            objRef?.Dispose();
            hubConnection.DisposeAsync();
        }

        private async Task<IEnumerable<MessageContract>> GetMessagesAsync(int skip) =>
            await Http.GetAsync<IEnumerable<MessageContract>>($"api/messages?chatId={ChatId}&Skip={skip}&Top={DefaultMessagesTop}");
    }
}