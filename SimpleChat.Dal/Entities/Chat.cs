using SimpleChat.Dal.Interfaces;
using System.Collections.Generic;

namespace SimpleChat.Dal.Entities
{
    public class Chat : IDbEntity
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; } = true;

        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    }
}
