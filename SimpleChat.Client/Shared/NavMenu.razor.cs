using Microsoft.AspNetCore.Components;
using SimpleChat.Shared.Contracts;
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

        [Inject]
        private HttpClient Http { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var publicChats = await Http.GetFromJsonAsync<IEnumerable<ChatContract>>("api/chats");

            if (publicChats?.Any() == true)
            {
                chats.AddRange(publicChats);
            }
        }
    }
}
