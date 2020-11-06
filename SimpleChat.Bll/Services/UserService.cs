using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task<int> CreateAsync(UserModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllAsync(int skip, int top)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(UserModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
