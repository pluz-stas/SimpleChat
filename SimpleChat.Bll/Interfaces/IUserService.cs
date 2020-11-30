using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Interfaces
{
    public interface IUserService : IService<UserModel, User>
    {
        Task<IEnumerable<UserModel>> GetByChatIdAsync(int chatId, int skip, int top);
        Task<UserModel> GetBySissionKeyAsync(string SessionKey);
    }
}
