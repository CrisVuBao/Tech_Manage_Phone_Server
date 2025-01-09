using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_Manage_Server.Application.DTOs.ImageDto
{
    public class ImageSourceDto
    {
        public int ImageId { get; set; }

        public string? FileName { get; set; } = null;
        public string FileExtention { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
