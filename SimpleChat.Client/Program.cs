using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Client.Infrastructure;
using SimpleChat.Client.Infrastructure.Extensions;
using SimpleChat.Client.Services;

namespace SimpleChat.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddSingleton<ErrorStateService>();
            builder.Services.AddSingleton<LoadFileService>();
            builder.Services.AddLocalization();
            builder.Services.ConfigureAppsettingsOptions(builder.Configuration);

            var host = builder.Build();

            await host.SetDefaultOptions();

            await host.RunAsync();
        }
    }
}
