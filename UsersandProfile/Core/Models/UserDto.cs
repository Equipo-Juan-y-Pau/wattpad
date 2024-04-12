using System.ComponentModel.DataAnnotations;

namespace UsersandProfile.Models
{
    public class UserDto{
        [Required]
        public string? Username { get; set;}

        [Required]
        public string? Email { get; set;}

        [Required]
        public string? Password { get; set;}
    }
}