using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleChat.Dal.Entities
{
    public class User
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserChat> UserChats { get; set; }
    }
}
