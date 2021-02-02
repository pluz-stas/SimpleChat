using System.ComponentModel.DataAnnotations;

namespace SimpleChat.Shared.Contracts.Message
{
    public class CreateMessageContract
    {
        [Required]
        [MaxLength(100)]
        public string Content { get; set; }
        
        [Required]
        public ShortUserInfoContract User { get; set; }
    }
}
