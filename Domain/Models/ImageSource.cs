using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_Manage_Server.Domain.Models
{
    public class ImageSource
    {
        [Key]
        public int ImageId { get; set; }

        public string? FileName { get; set; }
        public string FileExtention { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
