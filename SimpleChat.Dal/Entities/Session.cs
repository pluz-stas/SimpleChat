using SimpleChat.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.Dal.Entities
{
    public class Session : IDbEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public DateTime Expires { get; set; }

        public User User;
    }
}
