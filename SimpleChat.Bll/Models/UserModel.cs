using System.Collections.Generic;

namespace SimpleChat.Bll.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ChatModel> Chats { get; set; } = new List<ChatModel>();
    }
}
