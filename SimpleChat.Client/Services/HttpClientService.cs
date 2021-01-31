using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;


namespace SimpleChat.Client.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _client;
        private readonly ErrorStateService _errorState;
        private readonly NavigationManager _navigationManager;

        public HttpClientService(HttpClient client, ErrorStateService errorState, NavigationManager navigationManager)
        {
            _client = client;
            _errorState = errorState;
            _navigationManager = navigationManager;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response;

            try
            {
                response = await _client.GetAsync(uri);
            }
            catch
            {
                throw;
            }

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            await GetError(response);

            return default;
        }

        public async Task PostAsync<T>(string uri, T value)
        {
            HttpResponseMessage response;

            try
            {
                response = await _client.PostAsJsonAsync(uri, value);
            }
            catch
            {
                throw;
            }

            if (!response.IsSuccessStatusCode)
            {
                await GetError(response);
            }
        }

        private async Task GetError(HttpResponseMessage response)
        {
            string message;

            try
            {
                message = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                message = "Something went wrong..";
            }

            _errorState.SetError("Error", message);
            _navigationManager.NavigateTo(_navigationManager.BaseUri);
        }
    }
}