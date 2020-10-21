using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleChat.Dal.Entities
{
    class Message
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }

    }
}
