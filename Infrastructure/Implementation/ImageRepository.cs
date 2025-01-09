using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Domain.Interface;
using Tech_Manage_Server.Domain.Models;

namespace Tech_Manage_Server.Infrastructure.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ManageDBContext _context;

        public ImageRepository( IHttpContextAccessor httpContextAccessor, ManageDBContext context) 
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ImageSource> Upload(IFormFile file, ImageSource imageSource)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources/Images", $"{imageSource.FileName}{imageSource.FileExtention}");
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            // Update database
            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme} ://{httpRequest.Host}{httpRequest.PathBase}/Resources/Images{imageSource.FileName}{imageSource.FileExtention}";

            imageSource.Url = urlPath;

            await _context.ImageSources.AddAsync(imageSource);
            await _context.SaveChangesAsync();

            return imageSource;

        }
    }
}
