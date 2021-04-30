using System.Collections.Generic;

namespace SimpleChat.Bll.Models
{
    public class ChatModel
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        
        public bool IsMasterPassword { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string InviteLink { get; set; }
        public string NewPassword { get; set; }

        public IEnumerable<MessageModel> Messages { get; set; }
    }
}
