using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Bll.Services
{
    public class UserService : AbstractService<UserModel, User>, IUserService
    {
        public UserService(IUserRepository repository) : base (repository) { }
    }
}
