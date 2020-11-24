using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SimpleChat.Dal.Repository
{
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(SimpleChatDbContext context) : base(context)
        {
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            var user = await dbContext.Users.Include(x => x.Chats.Where(c => c.IsPublic)).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new NullReferenceException(nameof(User));

            return user;
        }
    }
}
