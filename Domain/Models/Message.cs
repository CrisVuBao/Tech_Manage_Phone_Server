using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tech_Manage_Server.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey("Sender")]
        public int? SenderId { get; set; } // ApplicationUser: Người gửi
        public ApplicationUser Sender { get; set; }

        [ForeignKey("Receiver")]
        public int? ReceiverId { get; set; } // ApplicationUser: Người nhận
        public ApplicationUser Receiver { get; set; }

        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;
    }
}
