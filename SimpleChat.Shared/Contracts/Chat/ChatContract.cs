using SimpleChat.Shared.Contracts.Message;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleChat.Shared.Attributes;

namespace SimpleChat.Shared.Contracts.Chat
{
    public class ChatContract
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public bool IsMasterPassword { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        public IEnumerable<MessageContract> Messages { get; set; } = new List<MessageContract>();
    }
}
