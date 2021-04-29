using System.ComponentModel.DataAnnotations;
using SimpleChat.Shared.Attributes;

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
        
        [MaxLength(16, ErrorMessage = "New password is too long.")]
        [MinLength(8, ErrorMessage = "New password is too short.")]
        [Password(ErrorMessage = "Invalid new password")]
        public string NewPassword { get; set; }
        
        
        public bool IsMasterPassword { get; set; }
    }
}
