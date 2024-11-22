using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.DTOs.InventoryModelDto
{
    public class CreateInventoryDto
    {
        [Required]
        public string InventoryName { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        public int ReorderLevel { get; set; } // cấp độ kho hàng, 1 là đầy kho, 2 là vừa, 3 hàng sắp hết

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
