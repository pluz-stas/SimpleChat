using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;

namespace SimpleChat.Bll.Interfaces
{
    public interface IUserService : IService<UserModel, User>
    {
    }
}
