using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
