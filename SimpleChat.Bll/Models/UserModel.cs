using System.Collections.Generic;
using System.Security.Claims;

namespace SimpleChat.Bll.Models
{
    public class UserModel : ClaimsPrincipal
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ChatModel> Chats { get; set; }
        public IEnumerable<SessionModel> Sessions { get; set; }
    }
}
