using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Client.Components.Modals
{
    public partial class UserImageModal
    {
        private IList<string> avatars = new List<string>();
        private int pages = 0;
        private int imagesCountPerPage = 0;
        private int currentPage = 0;
        private bool pageSizeCalculated = false;

        [Inject]
        private UserDataStorageService UserStorage { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public EventCallback OnImageModalClose { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetAvatarsAsync();

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                imagesCountPerPage = await JsRuntime.InvokeAsync<int>(JsFunctions.GetPicsCountPerPage);
                pageSizeCalculated = true;
                CalculateCount();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private Task GetAvatarsAsync()
        {
            avatars = new List<string>(); // here should be call on server to get pics.

            CalculateCount();

            return Task.CompletedTask;
        }

        private void CalculateCount() => pages = imagesCountPerPage == default ? default : avatars.Count / imagesCountPerPage;

        private async Task SelectAvatar(string path)
        {
            await UserStorage.SetUserPicAsync(path);

            await OnImageModalClose.InvokeAsync();
        }
    }
}
