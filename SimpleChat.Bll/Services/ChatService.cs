using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Bll.Services
{
    public class ChatService : AbstractService<ChatModel, Chat>, IChatService
    {
        public ChatService(IChatRepository repository) : base(repository) { }
    }
}
