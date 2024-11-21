using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class Invoice // xuất Hóa đơn
    {
        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("Repair")]
        public int RepairId { get; set; }
        public Repair Repair { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; } // "Cash", "Card", "Online"

        [Required]
        [MaxLength(10)]
        public string PaymentStatus { get; set; } // "Paid", "Unpaid"

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
