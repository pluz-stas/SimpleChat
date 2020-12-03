using System;

namespace SimpleChat.Shared.Contracts.Messages
{
    public class HubMessage
    {
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
    }
}
