using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.Bll.Services
{
    public class ChatService : IChatService
    {
        public Task<int> CreateAsync(ChatModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ChatModel>> GetAllAsync(int skip, int top)
        {
            throw new System.NotImplementedException();
        }

        public Task<ChatModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(ChatModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
