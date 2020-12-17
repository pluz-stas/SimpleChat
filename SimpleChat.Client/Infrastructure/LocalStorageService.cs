using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace SimpleChat.Client.Infrastructure
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime jsRuntime;
        private const string Set = "set";
        private const string Get = "get";
        private const string Remove = "remove";

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task SetAsync<T>(string key, T item) where T : class
        {
            var data = JsonSerializer.Serialize(item);
            await jsRuntime.InvokeVoidAsync(Set, key, data);
        }

        public Task SetStringAsync(string key, string value)
        {
            jsRuntime.InvokeAsync<string>(Set, key, value);
            return Task.CompletedTask;
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var data = await jsRuntime.InvokeAsync<string>(Get, key);
            return string.IsNullOrEmpty(data) ? null : JsonSerializer.Deserialize<T>(data);
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
