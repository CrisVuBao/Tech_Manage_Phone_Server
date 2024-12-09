using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.DTOs.RepairItemModelDto
{
    public class CreateRepairItemDto
    {
        [Required]
        public int InventoryID { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int Quantity { get; set; }
    }
}
