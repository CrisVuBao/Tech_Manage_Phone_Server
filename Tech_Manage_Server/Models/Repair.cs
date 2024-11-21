using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech_Manage_Server.Models
{
    public class Repair
    {
        [Key]
        public int RepairId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DeviceName { get; set; }

        public string? ErrorCondition { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Lend { get; set; } // cho mượn máy
        public DateTime? CreationDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Note { get; set; }
        public bool? IsDelete { get; set; }
        public string? Status { get; set; } // Received: Đã nhận, InProgress: Đang sửa, Completed: Đã xong

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<RepairItem> RepairItems { get; set; }
        public Invoice Invoice { get; set; } // hóa đơn
        public ICollection<Feedbacks> Feedbacks { get; set; }
    }
}
