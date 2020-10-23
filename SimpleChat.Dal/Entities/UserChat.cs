using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleChat.Dal.Entities
{
    public class UserChat
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
