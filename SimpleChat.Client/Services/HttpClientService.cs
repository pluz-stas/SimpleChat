using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using SimpleChat.Shared.Contracts;

namespace SimpleChat.Client.Services
{
    public class HttpClientService : IHttpClientService
    {
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
                await _errorState.SetErrorAsync("Error", "Something went wrong");
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
                await _errorState.SetErrorAsync("Error", "Something went wrong");
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
            string title;
            string message;
            
            try
            {
                var exception = JsonConvert.DeserializeObject<ExceptionContract>(
                    await response.Content.ReadAsStringAsync());
                title = exception.Title;
                message = exception.Message;
            }
            catch
            {
                title = "Something went Wrong";
                message = response.StatusCode.ToString();
            }
            await _errorState.SetErrorAsync(title, message);
        }
    }
}