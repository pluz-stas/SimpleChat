using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using SimpleChat.Shared;
using SimpleChat.Shared.Contracts;
using Microsoft.Extensions.Localization;


namespace SimpleChat.Client.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _client;
        private readonly ErrorStateService _errorState;
        private readonly IStringLocalizer<SharedExceptionResource> _localizer;

        public HttpClientService(HttpClient client, ErrorStateService errorState, IStringLocalizer<SharedExceptionResource> localizer)
        {
            _client = client;
            _errorState = errorState;
            _localizer = localizer;
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
                await _errorState.SetErrorAsync(_localizer["HttpClientErrorTitle"], _localizer["HttpClientErrorMessage"]);
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
                await _errorState.SetErrorAsync(_localizer["HttpClientErrorTitle"], _localizer["HttpClientErrorMessage"]);
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
                title = _localizer["ExceptionErrorTitle"];
                message = _localizer["ExceptionErrorMessage"];
            }
            await _errorState.SetErrorAsync(title, message);
        }
    }
}