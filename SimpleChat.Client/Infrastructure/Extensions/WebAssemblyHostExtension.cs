using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using SimpleChat.Client.Infrastructure.Settings;
using SimpleChat.Client.Resources;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Services.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Client.Infrastructure.Extensions
{
    public static class WebAssemblyHostExtension
    {
        public async static Task SetDefaultOptions(this WebAssemblyHost host)
        {
            var localStorageService = host.Services.GetRequiredService<ILocalStorageService>();
            var js = host.Services.GetRequiredService<IJSRuntime>();

            var getCultureTask = localStorageService.GetStringAsync(AppConstants.LocalStorageConstants.BlazorCulture);
            var getThemeTask = localStorageService.GetStringAsync(AppConstants.LocalStorageConstants.Theme);

            await Task.WhenAll(getCultureTask, getThemeTask);

            var culture = getCultureTask.Result ??
                host.Services.GetRequiredService<IOptions<CulturesConfiguration>>().Value.DefaultCulture;

            Enum.TryParse(getThemeTask.Result, out Themes theme);
            
            await js.InvokeVoidAsync(JsFunctions.SwitchTheme, theme.ToString());

            var cultureInfo = new CultureInfo(culture);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        public static void ConfigureAppsettingsOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CulturesConfiguration>(options =>
            {
                var cultureSection = configuration.GetSection(AppConstants.CultureConstants.Culture);

                options.Cultures = cultureSection.GetSection(AppConstants.CultureConstants.Available).GetChildren().Select(x => x.Value).ToArray();
                options.DefaultCulture = cultureSection.GetSection(AppConstants.CultureConstants.Default).Value;
            });
        }
    }
}
