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
        [MaxLength(50)]
        public string Name { get; set; }
        public List<UserChat> UserChats { get; set; }
    }
}
