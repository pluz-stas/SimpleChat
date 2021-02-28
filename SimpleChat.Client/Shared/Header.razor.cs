using Microsoft.AspNetCore.Components;

namespace SimpleChat.Client.Shared
{
    public partial class Header
    {
        [Parameter]
        public EventCallback OnUserModalClick { get; set; }
    }
}
