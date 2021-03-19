using System.Threading.Tasks;

namespace SimpleChat.Client.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string uri);
        Task PostAsync<T>(string uri, T model);
        Task PutAsync<T>(string uri, T model);
    }
}