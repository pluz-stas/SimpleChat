using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat.Shared.Hub;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Client.Pages
{
    public partial class Index : IDisposable
    {
        private HubConnection hubConnection;
        private List<string> messages = new List<string>();
        private string userInput;
        private string messageInput;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;


        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri(HubConstants.ChatUri))
                .Build();

            hubConnection.On<string, string>(HubConstants.ReceiveMessage, (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                messages.Add(encodedMsg);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        private async Task Send()
        {
            await hubConnection.SendAsync(HubConstants.SendMessage, userInput, messageInput);
            messageInput = string.Empty;
        }

        public void Dispose() => _ = hubConnection.DisposeAsync();
    }
}
