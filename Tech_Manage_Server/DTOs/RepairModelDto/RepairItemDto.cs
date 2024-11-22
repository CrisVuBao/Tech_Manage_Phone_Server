using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.DTOs.RepairModelDto
{
    public class RepairItemDto
    {
        [Required]
        public int InventoryId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
