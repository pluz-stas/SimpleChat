using SimpleChat.Bll.Extensions;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    public class ChatService : AbstractService<ChatModel, Chat>, IChatService
    {
        public ChatService(IChatRepository repository) : base(repository) { }

        public override async Task<IEnumerable<ChatModel>> GetAllAsync(int skip, int top, Func<Chat, ChatModel> entityToModelMapper)
        {
            return entityToModelMapper != null ? 
                await base.GetAllAsync(skip, top, entityToModelMapper) :
                (await _repository.GetAllAsync(skip, top))
                .Select(x =>
                {
                    var chatModel = x.ToModel();
                    chatModel.Messages = x.Messages.Select(m => m.ToModel());
                    return chatModel;
                });
        }

        public override async Task<ChatModel> GetByIdAsync(int id, Func<Chat, ChatModel> entityToModelMapper)
        {
            if (entityToModelMapper != null)
            {
                return await base.GetByIdAsync(id, entityToModelMapper);
            }

            var chatEntity = await _repository.GetByIdAsync(id);

            var chatModel = chatEntity.ToModel();
            chatModel.Messages = chatEntity.Messages.Select(m => m.ToModel());

            return chatModel;
        }
    }
}
