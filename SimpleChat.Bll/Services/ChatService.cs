using System;
using AutoMapper;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System.Threading.Tasks;
using SimpleChat.Dal.Resources;


namespace SimpleChat.Bll.Services
{
    public class ChatService : AbstractService<ChatModel, Chat>, IChatService
    {
        private IPasswordService _passwordService;
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
    }
}
