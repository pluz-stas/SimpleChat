using Microsoft.AspNetCore.Components;
using SimpleChat.Client.Services;

namespace SimpleChat.Client.Shared
{
    public partial class Header
    {
        [Inject]
        private UserDataStorageService UserStorage { get; set; }

        [Parameter]
        public EventCallback OnUserModalClick { get; set; }
    }
}
