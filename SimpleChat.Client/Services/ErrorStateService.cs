using System;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleChat.Shared.Contracts;

namespace SimpleChat.Client.Services
{
    public class ErrorStateService
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public event Action OnChange;


        public async Task SetErrorAsync(string title, string message)
        {
            Message = message;
            Title = title;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}