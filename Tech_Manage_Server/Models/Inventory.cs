using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class Inventory // Kho linh kiện
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        public string Description { get; set; }

        public int QuantityInStock { get; set; }

        public int ReorderLevel { get; set; } // cấp độ kho hàng, 1 là đầy kho, 2 là vừa, 3 hàng sắp hết

        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<RepairItem> RepairItems { get; set; }
    }
}
