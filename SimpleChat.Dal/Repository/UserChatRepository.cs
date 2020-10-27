using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Dal.Repository
{
    public class UserChatRepository : AbstractRepository<UserChat>, IUserChatRepository
    {
        public UserChatRepository(SimpleChatDbContext context) : base(context)
        {
        }
    }
}
