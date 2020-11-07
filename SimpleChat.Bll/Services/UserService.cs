using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;
using SimpleChat.Dal.Interfaces;

namespace SimpleChat.Bll.Services
{
    class UserService : AbstractService<User, UserModel>, IUserService
    {
        public UserService(IUserRepository repository) : base (repository) { }
    }
}
