using System;
using Microsoft.AspNetCore.Components;
using SimpleChat.Client.Services;

namespace SimpleChat.Client.Components
{
    
    public partial class CustomError : IDisposable
    {
        private bool isShowError = true;

        [Inject]
        private ErrorStateService ErrorState { get; set; }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ErrorState.OnChange += StateHasChanged;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            isShowError = true;
        }

        private void CloseError()
        {
            isShowError = false;
            ErrorState.ClearError();
        }

        public void Dispose() => ErrorState.OnChange -= StateHasChanged;
    }
}