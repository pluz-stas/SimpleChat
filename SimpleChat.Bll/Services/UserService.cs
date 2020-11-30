using SimpleChat.Bll.Extensions;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    public class UserService : AbstractService<UserModel, User>, IUserService
    {
        public UserService(IUserRepository repository) : base (repository) { }
        public override async Task<UserModel> GetByIdAsync(int id, System.Func<User, UserModel> entityToModelMapper)
        {
            if (entityToModelMapper != null)
            {
                return await base.GetByIdAsync(id, entityToModelMapper);
            }

            var userEntity = await _repository.GetByIdAsync(id);

            var userModel = userEntity.ToModel();
            userModel.Chats = userEntity.Chats.Select(u => u.ToModel());

            return userModel;
        }
        public async Task<IEnumerable<UserModel>> GetByChatIdAsync(int chatId, int skip, int top) => 
            (await _repository.FilterAsync(x => x.Chats.Any(c => c.Id == chatId))).Select(x => x.ToModel());

        public async Task<UserModel> GetBySissionKeyAsync(string SessionKey) =>
            (await _repository.FilterAsync(x => x.Sessions.Any(s => s.Key == SessionKey))).FirstOrDefault().ToModel();
    }
}
