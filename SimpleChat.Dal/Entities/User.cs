using SimpleChat.Dal.Interfaces;
using System.Collections.Generic;

namespace SimpleChat.Dal.Entities
{
    public class User : IDbEntity
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }

        public IEnumerable<UserChat> UserChats { get; set; }
    }
}
