using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Shared.Contracts.Message;

namespace SimpleChat.Client.Components
{
    public partial class MessageComponent
    {
        private const string DefaultAvatar = "images/defaultAvatar.jpg";

        private string userId;
        
        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }
        
        [Parameter]
        public MessageContract Message { get; set; }
        
        [Parameter]
        public bool DisplayAvatar { get; set; }
        
        [Parameter]
        public bool DisplayUserName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userId = await LocalStorageService.GetStringAsync(LocalStorageAttributes.UserIdKeyName);
        }
    }
}