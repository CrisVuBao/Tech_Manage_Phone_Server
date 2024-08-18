using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.DTOs
{
    public class RepairDto
    {
        public string DeviceName { get; set; }
        public string ErrorCondition { get; set; }
        public string CurrentStatus { get; set; } // hiện trạng
        public string ImageUrl { get; set; }
        public bool Lend { get; set; } // cho mượn máy
        public DateTime CreationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }
        public bool IsDelete { get; set; }
        public string Status { get; set; } // Received: Đã nhận, InProgress: Đang sửa, Completed: Đã xong
        public Customers Customers { get; set; }
    }
}
