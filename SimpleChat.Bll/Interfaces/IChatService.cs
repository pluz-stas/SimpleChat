using System.Threading.Tasks;
using SimpleChat.Bll.Models;
using SimpleChat.Dal.Entities;

namespace SimpleChat.Bll.Interfaces
{
    public interface IChatService : IService<ChatModel, Chat>
    {
        Task<ChatModel> GetByInviteLinkAsync(string inviteLink);
    }
}
