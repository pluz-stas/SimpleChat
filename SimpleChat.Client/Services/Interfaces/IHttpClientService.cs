using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleChat.Client.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string uri);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T model);
        Task PutAsync<T>(string uri, T value);
    }
}