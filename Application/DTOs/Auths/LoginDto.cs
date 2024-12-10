using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.DTOs.Auths
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } // Có thể là Email hoặc Số điện thoại

        [Required]
        public string Password { get; set; }
    }
}
