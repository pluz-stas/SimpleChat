using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Resources;
using SimpleChat.Client.Resources.Constants;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SimpleChat.Client.Shared
{
    public partial class Header
    {
        private Themes theme;
        private CultureInfo culture = CultureInfo.CurrentCulture;
        private bool isCultureMenuOpen = false;

        private readonly CultureInfo[] cultures =
        {
            new CultureInfo(AppConstants.DefaultCulture),
            new CultureInfo("ru-RU")
        };

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Inject]
        private ILocalStorageService Storage { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            theme = Enum.TryParse<Themes>(await Storage.GetStringAsync(AppConstants.Theme), out var storageTheme) ?
                storageTheme : Themes.Light;

            await JsRuntime.InvokeVoidAsync(JsFunctions.SwitchTheme, theme.ToString());
        }

        private async Task SwitchTheme()
        {
            theme = theme switch
            {
                Themes.Dark => Themes.Light,
                Themes.Light => Themes.Dark,
                _ => Themes.Dark
            };

            await Storage.SetStringAsync(AppConstants.Theme, theme.ToString());
            await JsRuntime.InvokeVoidAsync(JsFunctions.SwitchTheme, theme.ToString());
        }

        private string GetThemeIcon() => theme switch
        {
            Themes.Light => "oi-moon",
            Themes.Dark => "oi-sun",
            _ => "oi-sun",
        };

        private async Task SelectCulture(CultureInfo culture)
        {
            isCultureMenuOpen = false;

            if (!CultureInfo.CurrentCulture.Name.Equals(culture.Name))
            {
                await Storage.SetStringAsync(AppConstants.BlazorCulture, culture.Name);

                NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
            }
        }

        private string GetCultureIcon(CultureInfo culture) => $"/cultures/{culture.Name}.png";
    }
}
