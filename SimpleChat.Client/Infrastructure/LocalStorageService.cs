using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace SimpleChat.Client.Infrastructure
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

        public Task SetStringAsync(string key, string value)
        {
            jsRuntime.InvokeAsync<string>(Set, key, value);
            return Task.CompletedTask;
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await jsRuntime.InvokeAsync<string>(Get, key);
        }

        public Task RemoveAsync(string key)
        {
            jsRuntime.InvokeAsync<string>(Remove, key);
            return Task.CompletedTask;
        }
    }
}
