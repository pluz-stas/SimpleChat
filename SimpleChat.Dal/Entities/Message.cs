﻿using SimpleChat.Dal.Interfaces;
using System;

namespace SimpleChat.Dal.Entities
{
    public class Message : IDbEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
