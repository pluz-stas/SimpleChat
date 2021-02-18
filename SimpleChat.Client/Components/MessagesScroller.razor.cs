using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleChat.Client.Helpers;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Shared.Contracts.Message;

namespace SimpleChat.Client.Components
{
    public partial class MessagesScroller : IDisposable
    {
        private const string UserIdKeyName = "UserId";
        private const string DefaultAvatar = "images/defaultAvatar.jpg";
        
        private DotNetObjectReference<PaginationHelper> objRef;
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }
        [Parameter]
        public List<MessageContract> Messages { get; set; }

        private string UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserId = await LocalStorageService.GetStringAsync(UserIdKeyName);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            objRef = DotNetObjectReference.Create(new PaginationHelper("kek"));

            JsRuntime.InvokeAsync<string>("addPaginationEvent", objRef);
        }
        
        public void Dispose()
        {
            objRef?.Dispose();
        }
    }
}