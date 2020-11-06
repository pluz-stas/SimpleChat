using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    public class ChatService : AbstractService<Chat, ChatModel>, IChatService
    {
        private readonly IChatRepository repository;

        public ChatService(IChatRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
