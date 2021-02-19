using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleChat.Client.Infrastructure.Settings;
using SimpleChat.Client.Resources.Constants;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Client.Infrastructure.Extensions
{
    public static class WebAssemblyHostExtension
    {
        public async static Task SetDefaultCulture(this WebAssemblyHost host)
        {
            var localStorageService = host.Services.GetRequiredService<ILocalStorageService>();
            var culture = (await localStorageService.GetStringAsync(AppConstants.LocalStorageConstants.BlazorCulture)) ??
                host.Services.GetRequiredService<IOptions<CulturesConfiguration>>().Value.DefaultCulture;

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
