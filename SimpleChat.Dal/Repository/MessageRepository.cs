using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Dal.Repository
{
    public class MessageRepository : AbstractRepository<Message>, IMessageRepository
    {
        public MessageRepository(SimpleChatDbContext context) : base(context)
        {
        }
    }
}
