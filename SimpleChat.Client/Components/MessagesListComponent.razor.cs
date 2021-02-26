﻿using System;
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
using static System.String;

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

        private bool IsDateSplit(List<MessageContract> messages, int messageId)
        {
            var previousMessage = messageId + 1 < messages.Count ? messages[messageId + 1] : null;

            if (previousMessage != null)
                return previousMessage.CreatedDate.ToLocalTime().Day !=
                       messages[messageId].CreatedDate.ToLocalTime().Day;

            return false;
        }

        private bool IsUserName(List<MessageContract> messages, int messageId, string uid)
        {
            var previousMessage = messageId + 1 < messages.Count ? messages[messageId + 1] : null;
            var previousMessageUid = previousMessage?.User.UserId;

            return uid != previousMessageUid || previousMessageUid == null;
        }


        private bool IsDefaultAvatar(List<MessageContract> messages, int messageId, string uid)
        {
            var nextMessage = messageId > 0 ? messages[messageId - 1] : null;
            var nextMessageUid = nextMessage?.User.UserId;
            if (IsNullOrEmpty(messages[messageId].User.UserImg) && uid == null)
                return true;
            if (uid != nextMessageUid) 
                return false;
            
            messages[messageId].User.UserImg = null;
            return false;

        }

        public void Dispose()
        {
            objRef?.Dispose();
            hubConnection.DisposeAsync();
        }

        private async Task<IEnumerable<MessageContract>> GetMessagesAsync(int skip) =>
            await Http.GetAsync<IEnumerable<MessageContract>>(
                $"api/messages?chatId={ChatId}&Skip={skip}&Top={DefaultMessagesTop}");
    }
}