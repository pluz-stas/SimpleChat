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

        public override Task<IEnumerable<Message>> GetAllAsync(int skip, int top) => throw new NotImplementedException();

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

        public override async Task<IEnumerable<Message>> FilterAsync(Expression<Func<Message, bool>> predicate) =>
            await dbContext.Set<Message>()
            .Include(x => x.User)
            .Include(x => x.Chat)
            .AsNoTracking().Where(predicate).ToListAsync();

        public override async Task<int> CreateAsync(Message model)
        {
            var userId = model.User?.Id ?? throw new ArgumentNullException();

            model.User = await dbContext.Users.FindAsync(userId);

            return await base.CreateAsync(model);
        }

        public override async Task UpdateAsync(Message model)
        {
            var userId = model.User?.Id ?? throw new ArgumentNullException();

            model.User = await dbContext.Users.FindAsync(userId);

            await base.UpdateAsync(model);
        }
    }
}
