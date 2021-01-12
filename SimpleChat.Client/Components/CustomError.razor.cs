using Microsoft.AspNetCore.Components;
using SimpleChat.Client.Services;

namespace SimpleChat.Client.Components
{
    
    public partial class CustomError
    {
        [Inject]
        private ErrorStateService ErrorState { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ErrorState.OnChange += StateHasChanged;
        }
    }
}