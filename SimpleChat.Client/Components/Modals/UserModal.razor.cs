using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Infrastructure.Settings;
using SimpleChat.Client.Resources;
using SimpleChat.Client.Resources.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Client.Components.Modals
{
    public partial class UserModal
    {
        private const string UserNameKeyName = "UserName";
        private const string UserImgKeyName = "UserImgUrl";

        private bool isUserNameInputOpen;
        private bool isUserImgInputOpen;

        private string UserName { get; set; }
        private string UserImg { get; set; }
        
        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string localStorageName = await LocalStorageService.GetStringAsync(UserNameKeyName);
            UserName = string.IsNullOrWhiteSpace(localStorageName) ? "anon" : localStorageName;
            UserImg = await LocalStorageService.GetStringAsync(UserImgKeyName);

            cultures = Options.Value.Cultures.Select(x => new CultureInfo(x));

            Enum.TryParse(await Storage.GetStringAsync(AppConstants.LocalStorageConstants.Theme), out theme);
        }

        private async Task SetNameAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                await LocalStorageService.SetStringAsync(UserNameKeyName, UserName);
            }
        }

        private async Task SetImgAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserImg))
                await LocalStorageService.SetStringAsync(UserImgKeyName, UserImg);
        }

        private async Task ToggleEditUserNameInputAsync()
        {
            if (isUserNameInputOpen)
            {
                await SetNameAsync();
            }
            isUserNameInputOpen = !isUserNameInputOpen;
            isUserImgInputOpen = !isUserImgInputOpen && isUserImgInputOpen;
        }

        private async Task ToggleEditUserImgInputAsync()
        {
            if (isUserImgInputOpen)
            {
                await SetImgAsync();
            }
            isUserImgInputOpen = !isUserImgInputOpen;
            isUserNameInputOpen = isUserNameInputOpen && !isUserNameInputOpen;
        }

        private Themes theme;
        private CultureInfo culture = CultureInfo.CurrentCulture;
        private bool isCultureMenuOpen = false;
        private IEnumerable<CultureInfo> cultures;

        [Inject]
        private IOptions<CulturesConfiguration> Options { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Inject]
        private ILocalStorageService Storage { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }

        [Parameter]
        public EventCallback OnUserModalClick { get; set; }

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
