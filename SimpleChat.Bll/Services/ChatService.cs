using System;
using AutoMapper;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System.Threading.Tasks;
using SimpleChat.Dal.Resources;
using SimpleChat.Shared.Exceptions;


namespace SimpleChat.Bll.Services
{
    public class ChatService : AbstractService<ChatModel, Chat>, IChatService
    {
        private readonly IPasswordService _passwordService;
        public ChatService(IChatRepository repository, IMapper mapper, IPasswordService passwordService) : base(repository, mapper)
        {
            _passwordService = passwordService;
        }
        
        public override async Task<int> CreateAsync(ChatModel chatModel)
        {

            var chatEntity = _mapper.Map<Chat>(chatModel);
            if(!string.IsNullOrEmpty(chatModel.Password))
                if (_passwordService.Validate(chatModel.Password))
                    chatEntity.PasswordHash = _passwordService.Hash(chatModel.Password);
                else
                    throw new ArgumentException(string.Format(ErrorDetails.IncorrectChatPasswordFormat));

            return await _repository.CreateAsync(chatEntity);
        }
        
        public override async Task UpdateAsync(ChatModel model)
        {
            var chatEntity = await _repository.GetByIdAsync(model.Id);
            
            if (chatEntity == null)
            {
                throw new NotFoundException(string.Format(ErrorDetails.ChatDoesNotExist, model.Id));
            }

            if (!string.IsNullOrEmpty(chatEntity.PasswordHash))
            {
                if (string.IsNullOrEmpty(model.Password) || !_passwordService.Verify(chatEntity.PasswordHash, model.Password))
                {
                    throw new ArgumentException(string.Format(ErrorDetails.InvalidPassword));
                }
            }

            if (model.IsMasterPassword)
            {
                model.PasswordHash = chatEntity.PasswordHash;
            }

            if (!string.IsNullOrEmpty(model.NewPassword))
                if (_passwordService.Validate(model.NewPassword))
                    model.PasswordHash = _passwordService.Hash(model.NewPassword);
                else
                    throw new ArgumentException(string.Format(ErrorDetails.IncorrectChatPasswordFormat));

            await _repository.UpdateAsync(_mapper.Map<Chat>(model));
        }
    }
}
