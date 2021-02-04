using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleChat.Dal.Entities;

namespace SimpleChat.Dal.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetByChatAsync(int chatId, int skip, int top);
    }
}
