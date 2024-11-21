using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class AuditLog // kiểm tra hoạt động của các user
    {
        [Key]
        public int LogId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? UserId { get; set; } // Có thể null nếu là hệ thống tự ghi
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
