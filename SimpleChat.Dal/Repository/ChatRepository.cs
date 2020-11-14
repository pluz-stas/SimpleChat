using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleChat.Dal.Repository
{
    public class ChatRepository : AbstractRepository<Chat>, IChatRepository
    {
        private const int DefaultMessagesTop = 50;

        public ChatRepository(SimpleChatDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Chat>> GetAllAsync(int skip, int top)
        {
            IQueryable<Chat> chats = dbContext.Chats.AsNoTracking()
                .Include(x => x.Messages)
                .Where(x => x.IsPublic).Skip(skip).Take(top);

            await chats.ForEachAsync(x => x.Messages = x.Messages.OrderByDescending(m => m.CreatedDate).Take(1));

            return await chats.ToListAsync();
        }

        public override async Task<Chat> GetByIdAsync(int id)
        {
            var model = await dbContext.Chats
                .Include(m => m.UserChats)
                .ThenInclude(s => s.User)
                .Include(x => x.Messages)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                throw new NullReferenceException(nameof(Chat));

            return model;
        }

        public override async Task<int> GetCountAsync() => await dbContext.Chats.Where(x => x.IsPublic).CountAsync();

        public override async Task<IEnumerable<Chat>> FilterAsync(Expression<Func<Chat, bool>> predicate)
        {
            IQueryable<Chat> chats = dbContext.Chats.AsNoTracking()
                .Include(x => x.Messages)
                .Where(x => x.IsPublic).Where(predicate);

            await chats.ForEachAsync(x => x.Messages = x.Messages.OrderByDescending(m => m.CreatedDate).Take(1));

            return await chats.ToListAsync();
        }
    }
}
