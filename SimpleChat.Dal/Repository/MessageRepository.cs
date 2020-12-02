using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
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
                .Include(x => x.Chat)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                throw new NullReferenceException(nameof(Message));

            return model;
        }
    }
}
