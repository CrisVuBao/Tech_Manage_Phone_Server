using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech_Manage_Server.Domain.Models;

namespace Tech_Manage_Server.Domain.Interface
{
    public interface IImageRepository
    {
        Task<ImageSource> Upload(IFormFile file, ImageSource imageSource);
    }
}
