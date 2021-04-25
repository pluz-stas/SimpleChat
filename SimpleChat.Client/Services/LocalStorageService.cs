using System.Threading.Tasks;
using Microsoft.JSInterop;
using SimpleChat.Client.Services.Interfaces;

namespace SimpleChat.Client.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private const string Set = "set";
        private const string Get = "get";
        private const string Remove = "remove";

        private readonly IJSRuntime jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task SetStringAsync(string key, string value) => await jsRuntime.InvokeAsync<string>(Set, key, value);

        public async Task<string> GetStringAsync(string key) => await jsRuntime.InvokeAsync<string>(Get, key);

        public async Task RemoveAsync(string key) => await jsRuntime.InvokeAsync<string>(Remove, key);
    }
}
