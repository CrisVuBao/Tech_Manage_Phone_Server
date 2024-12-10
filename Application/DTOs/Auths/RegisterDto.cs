using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.DTOs.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
