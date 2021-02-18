using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Client.Resources.Constants;
using System.Globalization;
using System.Threading.Tasks;

namespace SimpleChat.Client.Infrastructure.Extensions
{
    public static class WebAssemblyHostExtension
    {
        public async static Task SetDefaultCulture(this WebAssemblyHost host)
        {
            var localStorageService = host.Services.GetRequiredService<ILocalStorageService>();
            var culture = (await localStorageService.GetStringAsync(AppConstants.BlazorCulture)) ?? AppConstants.DefaultCulture;
            var cultureInfo = new CultureInfo(culture);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
