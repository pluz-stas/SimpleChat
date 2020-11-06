using System;

namespace SimpleChat.Bll.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserModel User { get; set; }
        public ChatModel Chat { get; set; }
    }
}
