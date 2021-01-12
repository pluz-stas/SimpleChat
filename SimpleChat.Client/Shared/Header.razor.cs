using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleChat.Client.Resources;
using SimpleChat.Client.Resources.Constants;
using System.Threading.Tasks;

namespace SimpleChat.Client.Shared
{
    public partial class Header
    {
        private Themes theme = Themes.Light;

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await JsRuntime.InvokeVoidAsync(JsFunctions.SwitchTheme, theme.ToString());
        }

        private void SwitchTheme()
        {
            theme = theme switch
            {
                Themes.Dark => Themes.Light,
                Themes.Light => Themes.Dark
            };

            JsRuntime.InvokeVoidAsync(JsFunctions.SwitchTheme, theme.ToString());
        }

        private string GetThemeIcon() => theme switch
            {
                Themes.Light => "oi-moon",
                Themes.Dark => "oi-sun",
            };
    }
}
