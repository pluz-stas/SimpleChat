using System.Collections.Generic;

namespace SimpleChat.Shared.Contracts
{
    public class Chat
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }

        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    }
}
