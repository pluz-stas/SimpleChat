using System.Threading.Tasks;
using SimpleChat.Dal.Entities;

namespace SimpleChat.Dal.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<Chat> GetByPrivateIdAsync(string inviteLink);
    }
}
