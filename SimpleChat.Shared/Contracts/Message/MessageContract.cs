using SimpleChat.Shared.Contracts.Chat;
using System;

namespace SimpleChat.Shared.Contracts.Message
{
    public class MessageContract
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ShortChatInfoContract Chat { get; set; }
        public ShortUserInfoContract User { get; set; }
    }
}
