using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Interfaces
{
    public interface IMessageService : IService<MessageModel, Message>
    {
        Task<IEnumerable<MessageModel>> GetByChatIdAsync(int chatId, int skip, int top);
    }
}
