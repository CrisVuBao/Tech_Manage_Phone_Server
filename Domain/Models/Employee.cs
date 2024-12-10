using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [MaxLength(50)]
        public string Position { get; set; } // chức vụ công việc

        public decimal Salary { get; set; } //lương

        // Navigation properties
        public ICollection<Repair> Repairs { get; set; }
        public ICollection<AuditLog> AuditLogs { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Notifications> Notifications { get; set; }
    }
}
