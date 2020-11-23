using System.Collections.Generic;

namespace SimpleChat.Bll.Models
{
    public class ChatModel
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }

        public IEnumerable<MessageModel> Messages { get; set; }

        public IEnumerable<UserModel> Users { get; set; }
    }
}
