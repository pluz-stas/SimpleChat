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
    public class MessageRepository : AbstractRepository<Message>, IMessageRepository
    {
        private const int DefaultMessagesTop = 50;

        public MessageRepository(SimpleChatDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Message>> GetAllAsync(int skip, int top)
        {
            IQueryable<Message> messages = dbContext.Messages.AsNoTracking()
                .Include(x => x.Chat)
                .Include(s => s.User)
                .Skip(skip).Take(top);

            return await messages.ToListAsync();
        }

        public override async Task<Message> GetByIdAsync(int id)
        {
            var model = await dbContext.Messages
                .Include(x => x.User)
                .Include(x => x.Chat)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                throw new NullReferenceException(nameof(Message));

            return model;
        }
    }
}
