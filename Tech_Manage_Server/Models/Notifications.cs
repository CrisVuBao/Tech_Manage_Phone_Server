using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class Notifications // Thông báo
    {
        [Key]
        public int NotificationId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;
    }
}
