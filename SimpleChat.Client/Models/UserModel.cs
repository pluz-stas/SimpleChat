using System.ComponentModel.DataAnnotations;

namespace SimpleChat.Client.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Too Long")]
        public string Name { get; set; }

        public string ImgUrl { get; set; }
    }
}