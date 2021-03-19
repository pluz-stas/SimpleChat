using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using SimpleChat.Client.Resources.ResourceFiles;

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
            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }

                await GetError(response);

                return default;
            }
            catch
            {
                _errorState.SetError(Resource.Error, Resource.RequestError);
                throw;
            }
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
                _errorState.SetError(Resource.Error, Resource.RequestError);
                throw;
            }

            if (!response.IsSuccessStatusCode)
            {
                await GetError(response);
            }
        }

        public async Task PutAsync<T>(string uri, T value)
        {
            HttpResponseMessage response;

            try
            {
                response = await _client.PutAsJsonAsync(uri, value);
            }
            catch
            {
                _errorState.SetError(Resource.Error, Resource.RequestError);
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
                _errorState.SetError(Resource.Error, Resource.RequestError);
                message = Resource.ErrorMessage;
            }

            _errorState.SetError(Resource.Error, message);
            _navigationManager.NavigateTo(_navigationManager.BaseUri);
        }
    }
}