using System.ComponentModel.DataAnnotations;

namespace SimpleChat.Shared.Contracts.Chat
{
    public class EditChatContract
    {
        public int Id { get; set; }
        public bool IsPublic { get; set; }
        public byte[] Photo { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [MaxLength(16)]
        public string Password { get; set; }
    }
}
