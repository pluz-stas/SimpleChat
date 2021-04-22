using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure.Settings;
using SimpleChat.Client.Resources;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Services;
using SimpleChat.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Client.Components.Modals
{
    public partial class UserModal
    {
        private bool isUserNameInputOpen;
        private bool isUserImgModalOpen;
        private bool isCultureMenuOpen;
        private Themes theme;
        private CultureInfo culture = CultureInfo.CurrentCulture;
        private IEnumerable<CultureInfo> cultures;
        private string userName;
        private string userImg;

        [Inject]
        private UserDataStorageService UserStorage { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Inject]
        private IOptions<CulturesConfiguration> Options { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Inject]
        private ILocalStorageService Storage { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userName = UserStorage.UserName;
            userImg = UserStorage.UserPic;

            cultures = Options.Value.Cultures.Select(x => new CultureInfo(x));

            Enum.TryParse(await Storage.GetStringAsync(AppConstants.LocalStorageConstants.Theme), out theme);
        }

        private string GetUserNameStatus()
        {
            if (IsUserNameValid())
            {
                return "green";
            }
            else
            {
                return "red";
            }
        }

        private void ResetUserName()
        {
            userName = "kek";
        }

        private bool IsUserNameValid() => !string.IsNullOrWhiteSpace(userName) && userName.Length < 30;

        private async Task SetNameAsync() => await UserStorage.SetUserNameAsync(userName);

        private async Task SetImgAsync() => await UserStorage.SetUserPicAsync(userImg);

        private async Task ToggleEditUserNameInputAsync()
        {
            if (isUserNameInputOpen)
            {
                await SetNameAsync();
            }
            isUserNameInputOpen = false;
            //isUserImgInputOpen = !isUserImgInputOpen && isUserImgInputOpen;
        }

        private async Task ToggleEditUserImgInputAsync()
        {
            if (isUserImgModalOpen)
            {
                await SetImgAsync();
            }
            isUserImgModalOpen = !isUserImgModalOpen;
            isUserNameInputOpen = isUserNameInputOpen && !isUserNameInputOpen;
        }

        private async Task SwitchTheme()
        {
            theme = theme switch
            {
                Themes.Dark => Themes.Light,
                Themes.Light => Themes.Dark,
                _ => Themes.Dark
            };

            await Storage.SetStringAsync(AppConstants.LocalStorageConstants.Theme, theme.ToString());
            await JsRuntime.InvokeVoidAsync(JsFunctions.SwitchTheme, theme.ToString());
        }

        private string GetThemeIcon() => theme switch
        {
            Themes.Light => "oi-moon",
            Themes.Dark => "oi-sun",
            _ => "oi-sun",
        };

        private async Task SelectCultureAsync(CultureInfo culture)
        {
            isCultureMenuOpen = false;

            if (!CultureInfo.CurrentCulture.Name.Equals(culture.Name))
            {
                await Storage.SetStringAsync(AppConstants.LocalStorageConstants.BlazorCulture, culture.Name);

                NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
            }
        }

        private string GetCultureIcon(CultureInfo culture) => $"/cultures/{culture.Name}.png";
    }
}
