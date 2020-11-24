using System.Collections.Generic;

namespace SimpleChat.Shared.Contracts
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Chat> Chats { get; set; } = new List<Chat>();
    }
}
