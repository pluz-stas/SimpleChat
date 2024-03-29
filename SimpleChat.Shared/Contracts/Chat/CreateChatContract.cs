﻿using System.ComponentModel.DataAnnotations;

namespace SimpleChat.Shared.Contracts.Chat
{
    public class CreateChatContract
    {
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
