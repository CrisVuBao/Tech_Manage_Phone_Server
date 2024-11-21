using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class RepairItem // linh kiện sử dụng trong đơn hàng
    {
        [Key]
        public int RepairItemId { get; set; }

        [ForeignKey("Repair")]
        public int RepairId { get; set; }
        public Repair Repair { get; set; }

        [ForeignKey("Inventory")]
        public int ItemId { get; set; }
        public Inventory Inventory { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
