using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        public ChatService(IChatRepository repository, IMessageRepository messageRepository, IMapper mapper, IPasswordService passwordService) : base(repository, mapper)
        {
            _passwordService = passwordService;
            _chatRepository = repository;
            _messageRepository = messageRepository;
        }
        
        public override async Task<int> CreateAsync(ChatModel chatModel)
        {
            var chatEntity = _mapper.Map<Chat>(chatModel);
            if(!string.IsNullOrEmpty(chatModel.Password))
                if (_passwordService.Validate(chatModel.Password))
                    chatEntity.PasswordHash = _passwordService.Hash(chatModel.Password);
                else
                    throw new ArgumentException(string.Format(ErrorDetails.IncorrectChatPasswordFormat));
            if (!chatModel.IsPublic && !string.IsNullOrEmpty(chatModel.InviteLink))
            {
                chatEntity.PrivateId = chatModel.InviteLink.Split('-')[0];
                chatEntity.InviteLinkHash = _passwordService
                    .Hash(chatModel.InviteLink.Replace(chatEntity.PrivateId + "-", ""));
            }

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

        public async Task<ChatModel> GetByInviteLinkAsync(string inviteLink)
        {
            var privateId = inviteLink.Split("-")[0];
            var chatEntity = (await _chatRepository.GetByPrivateIdAsync(privateId));
            if (chatEntity == null)
            {
                throw new NotFoundException(ErrorDetails.ChatDoesNotExist);
            }

            if (!_passwordService.Verify(chatEntity.InviteLinkHash, inviteLink.Replace(privateId + "-", "")))
            {
                throw new NotFoundException(ErrorDetails.ChatDoesNotExist);
            }
            
            return _mapper.Map<ChatModel>(chatEntity);
        }
    }
}
