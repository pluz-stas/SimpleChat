using Microsoft.AspNetCore.Components;
using SimpleChat.Shared.Contracts.Chat;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SimpleChat.Client.Shared
{
    public partial class NavMenu
    {
        private List<ChatContract> chats = new List<ChatContract>();
        private int selectedChatId;

        [Inject]
        private HttpClient Http { get; set; }

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
        }

        private async Task ChatSelected(int chatId)
        {
            selectedChatId = chatId;
            await OnChatSelecting.InvokeAsync();
        }
    }
}
