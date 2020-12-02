using SimpleChat.Bll.Extensions;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    public class MessageService : AbstractService<MessageModel, Message>, IMessageService
    {
        public MessageService(IMessageRepository messageRepository) : base(messageRepository) { }

        public override Task<IEnumerable<MessageModel>> GetAllAsync(int skip, int top, Func<Message, MessageModel> entityToModelMapper = null) =>
            throw new NotImplementedException();

        public override async Task<MessageModel> GetByIdAsync(int id, Func<Message, MessageModel> entityToModelMapper = null)
        {
            if (entityToModelMapper != null)
            {
                return await base.GetByIdAsync(id, entityToModelMapper);
            }

            var messageEntity = await _repository.GetByIdAsync(id);

            var messageModel = messageEntity.ToModel();
            messageModel.Chat = messageEntity.Chat.ToModel();

            return messageModel;
        }

        public override async Task<int> CreateAsync(MessageModel model, Func<MessageModel, Message> modelToEntityMapper = null)
        {
            if (modelToEntityMapper != null)
            {
                return await base.CreateAsync(model, modelToEntityMapper);
            }

            var messageEntity = model.ToEntity();
            messageEntity.ChatId = model.Chat?.Id ?? throw new ArgumentNullException();

            return await _repository.CreateAsync(messageEntity);
        }

        public override async Task UpdateAsync(MessageModel model, Func<MessageModel, Message> modelToEntityMapper = null)
        {
            if (modelToEntityMapper != null)
            {
                await base.UpdateAsync(model, modelToEntityMapper);
            }

            var messageEntity = model.ToEntity();
            messageEntity.ChatId = model.Chat?.Id ?? throw new NotImplementedException();

            await _repository.UpdateAsync(messageEntity);
        }
    }
}
