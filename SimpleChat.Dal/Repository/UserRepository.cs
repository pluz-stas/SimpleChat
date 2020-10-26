using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Dal.Repository
{
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(SimpleChatDbContext context) : base(context)
        {
        }
    }
}
