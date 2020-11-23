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

        public override async Task<IEnumerable<Chat>> GetAllAsync(int skip, int top) =>
            await dbContext.Chats.AsNoTracking()
                .Include(x => x.Messages.OrderByDescending(m => m.CreatedDate).Take(1))
                .Where(x => x.IsPublic).OrderBy(x => x.Id)
                .Skip(skip).Take(top).ToListAsync();

        public override async Task<Chat> GetByIdAsync(int id)
        {
            var model = await dbContext.Chats
                .Include(c => c.Users)
                .Include(x => x.Messages.OrderByDescending(mes => mes.CreatedDate).Take(DefaultMessagesTop))
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                throw new NullReferenceException(nameof(Chat));

            return model;
        }

        public override async Task<int> GetCountAsync() => await dbContext.Chats.Where(x => x.IsPublic).CountAsync();

        public override async Task<IEnumerable<Chat>> FilterAsync(Expression<Func<Chat, bool>> predicate) =>
            await dbContext.Chats.AsNoTracking()
                .Include(x => x.Messages.OrderByDescending(m => m.CreatedDate).Take(1))
                .Where(x => x.IsPublic).Where(predicate).ToListAsync();
    }
}
