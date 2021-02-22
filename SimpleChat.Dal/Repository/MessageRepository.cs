using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using SimpleChat.Dal.Resources;
using SimpleChat.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Dal.Repository
{
    public class MessageRepository : AbstractRepository<Message>, IMessageRepository
    {
        private const int DefaultMessagesTop = 50;

        public MessageRepository(SimpleChatDbContext context) : base(context)
        {
        }

        public override Task<IEnumerable<Message>> GetAllAsync(int skip, int top) => 
            throw new NotImplementedException(string.Format(ErrorDetails.MethodDoesNotImplemented, nameof(GetAllAsync)));

        public override async Task<Message> GetByIdAsync(int id) =>
            await dbContext.Messages
                .AsNoTracking()
                .Include(x => x.Chat)
                .FirstOrDefaultAsync(x => x.Id == id) ??
            throw new NotFoundException(string.Format(ErrorDetails.ResourceDoesNotExist, nameof(Message), id));

        public async Task<IEnumerable<Message>> GetByChatAsync(int chatId, int skip, int top) =>
            await dbContext.Messages.AsNoTracking()
                .Where(x => x.ChatId == chatId).OrderByDescending(x => x.CreatedDate)
                .Skip(skip).Take(top).ToListAsync();
    }
}
