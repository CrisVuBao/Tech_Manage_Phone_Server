using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Tech_Manage_Server.Models;
using RequiredIf.Core;
using Tech_Manage_Server.DTOs.CustomerModelDto;
using Tech_Manage_Server.DTOs.RepairItemModelDto;

namespace Tech_Manage_Server.DTOs.RepairModelDto
{
    public class RepairDto
    {
        public int RepairId { get; set; }
        public string DeviceName { get; set; }
        public string ErrorCondition { get; set; }
        public string ImageUrl { get; set; }
        public bool Lend { get; set; } // cho mượn máy
        public DateTime CreationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }
        public bool IsDelete { get; set; }
        public string Status { get; set; } // Received: Đã nhận, InProgress: Đang sửa, Completed: Đã xong

        public CustomerDto Customer { get; set; }
        public Employee Employee { get; set; }
        public List<RepairItemDto> RepairItems { get; set; }
        public ICollection<Feedbacks> Feedbacks { get; set; }
    }
}
