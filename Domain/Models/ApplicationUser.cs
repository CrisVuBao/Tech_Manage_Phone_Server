using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        // Đảm bảo Email là duy nhất
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public override string Email { get; set; }

        // Đảm bảo PhoneNumber là duy nhất
        [Required]
        [Phone]
        [MaxLength(15)]
        public override string PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        [MaxLength(10)]
        public string Status { get; set; } = "Active"; // "Active", "Inactive"

        // Navigation properties
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Notifications> Notifications { get; set; }
    }
}
