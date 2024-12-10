using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.DTOs.CustomerModelDto
{
    public class UpdateCustomerDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
