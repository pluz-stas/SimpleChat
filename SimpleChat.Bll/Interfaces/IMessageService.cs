using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;

namespace SimpleChat.Bll.Interfaces
{
    public interface IMessageService : IService<MessageModel, Message>
    {
        Task<IEnumerable<MessageModel>> GetByChat(int chatId, int skip, int top);
    }
}
