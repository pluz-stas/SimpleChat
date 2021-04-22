using System.Threading.Tasks;

namespace SimpleChat.Client.Services.Interfaces
{
    public interface ILocalStorageService
    {
        Task SetStringAsync(string key, string value);
        
        Task<string> GetStringAsync(string key);
        
        Task RemoveAsync(string key);
    }
}
