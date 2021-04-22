using Microsoft.AspNetCore.Components;
using SimpleChat.Client.Services;
using SimpleChat.Shared.Contracts.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SimpleChat.Client.Shared
{
    public partial class NavMenu : IDisposable
    {
        private List<ChatContract> chats = new List<ChatContract>();

        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private ChatTracker ChatTracker { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }

        [Parameter]
        public EventCallback OnChatSelecting { get; set; }
        
        [Parameter]
        public EventCallback OnCreateChatModalClick { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var publicChats = await Http.GetFromJsonAsync<IEnumerable<ChatContract>>("api/chats");

            if (publicChats?.Any() == true)
            {
                chats.AddRange(publicChats);
            }

            ChatTracker.ChatSelected = () => StateHasChanged();
        }

        public void Dispose()
        {
            ChatTracker.ChatSelected = null;
        }
    }
}
