using RequiredIf.Core;
using System.ComponentModel.DataAnnotations;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.DTOs.RepairModelDto
{
    public class CreateRepairDto
    {
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

        // Thông tin khách hàng
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Yêu cầu tên đầy đủ khi tạo khách hàng mới.")]
        public string FullName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        // Tùy chọn tạo tài khoản
        public bool CreateAccount { get; set; } = false;

        // Thông tin đăng nhập (nếu tạo tài khoản)
        [RequiredIf("CreateAccount", true, ErrorMessage = "Yêu cầu mật khẩu khi tạo tài khoản.")]
        public string Password { get; set; }
        public List<RepairItemDto> RepairItems { get; set; }
    }
}
