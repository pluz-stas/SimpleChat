using SimpleChat.Shared.Contracts.Message;
using System.Collections.Generic;

namespace SimpleChat.Shared.Contracts.Chat
{
    public class ChatContract
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }

        public IEnumerable<MessageContract> Messages { get; set; } = new List<MessageContract>();
    }
}
