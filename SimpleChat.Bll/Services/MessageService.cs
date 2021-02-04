using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using SimpleChat.Dal.Resources;
using SimpleChat.Shared.Exceptions;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    public class MessageService : AbstractService<MessageModel, Message>, IMessageService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        public MessageService(IChatRepository chatRepository, IMessageRepository messageRepository, IMapper mapper) : base(messageRepository, mapper)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }

        public override async Task<int> CreateAsync(MessageModel model)
        {
            if (!await _chatRepository.IsExistsAsync(model.Chat.Id))
            {
                throw new NotFoundException(string.Format(ErrorDetails.ChatDoesNotExist, model.Chat.Id));
            }

            var messageEntity = _mapper.Map<Message>(model);
            messageEntity.Chat = null;

            return await _repository.CreateAsync(messageEntity);
        }

        public override async Task UpdateAsync(MessageModel model)
        {
            var messageEntity = await _repository.GetByIdAsync(model.Id);

            if (messageEntity == null)
            {
                throw new NotFoundException(string.Format(ErrorDetails.MessageDoesNotExist, model.Id));
            }

            messageEntity.Content = model.Content;

            await _repository.UpdateAsync(messageEntity);
        }

        public async Task<IEnumerable<MessageModel>> GetByChatAsync(int chatId, int skip, int top)  =>
            (await _messageRepository.GetByChatAsync(chatId, skip, top)).Select(x => _mapper.Map<Message, MessageModel>(x));

    }
}
