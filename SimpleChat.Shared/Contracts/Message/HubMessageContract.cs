using System;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Shared.Contracts.Message
{
    public class HubMessageContract
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserName { get; set; }

        public ShortUserInfoContract User { get; set; }
        public ShortChatInfoContract Chat { get; set; }
    }
}