using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SimpleChat.Client.Services;

namespace SimpleChat.Client.Components
{
    
    public partial class CustomError
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
            Console.WriteLine(isShowError);
            isShowError = false;
        }
    }
}