using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Dal.Repository
{
    public class ChatRepository : AbstractRepository<Chat>, IChatRepository
    {
        public ChatRepository(SimpleChatDbContext context) : base(context)
        {
        }
    }
}
