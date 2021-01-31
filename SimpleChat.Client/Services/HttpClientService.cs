using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SimpleChat.Client.Services
{
    public class HttpClientService : IHttpClientService
    {
        private const string Error = nameof(Error);
        private const string DefaultMessage = "Ooops! Something went wrong(..";

        private readonly HttpClient _client;
        private readonly ErrorStateService _errorState;

        public HttpClientService(HttpClient client, ErrorStateService errorState)
        {
            _client = client;
            _errorState = errorState;
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
            throw new Exception(response.StatusCode.ToString());
        }

        public async Task PostAsync<T>(string uri, T value)
        {
            JsonContent content = JsonContent.Create(value, mediaType: null);
            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(uri, content);
            }
            catch
            {
                throw;
            }

            if (!response.IsSuccessStatusCode)
            {
                await GetError(response);
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private async Task GetError(HttpResponseMessage response)
        {
            string title = Error;
            string message;
            
            try
            {
                message = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                message = DefaultMessage;
            }

            _errorState.SetError(title, message);
        }
    }
}