using AutoMapper;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System.Threading.Tasks;


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
            chatEntity.PasswordHash = _passwordService.Hash(chatModel.Password);

            return await _repository.CreateAsync(chatEntity);
        }
    }
}
