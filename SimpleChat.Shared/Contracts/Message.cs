using System;

namespace SimpleChat.Shared.Contracts
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
