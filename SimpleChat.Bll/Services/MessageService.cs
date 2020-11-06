using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Bll.Services
{
    public class MessageService : AbstractService<Message, MessageModel>, IMessageService
    {
        public MessageService(IMessageRepository messageRepository) : base (messageRepository) { }
    }
}
