using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class Feedbacks // Phản hồi khách hàng
    {
        [Key]
        public int FeedbackId { get; set; }

        [ForeignKey("Repair")]
        public int RepairId { get; set; }
        public Repair Repair { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
