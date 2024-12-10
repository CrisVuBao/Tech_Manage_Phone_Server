using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.DTOs.InventoryModelDto
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string InventoryName { get; set; }

        public string Description { get; set; }

        public int QuantityInStock { get; set; }

        public int ReorderLevel { get; set; } // cấp độ kho hàng, 1 là đầy kho, 2 là vừa, 3 hàng sắp hết

        public decimal Price { get; set; }
    }
}
