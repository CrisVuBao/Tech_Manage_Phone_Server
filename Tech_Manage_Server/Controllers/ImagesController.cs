using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tech_Manage_Server.Application.DTOs.ImageDto;
using Tech_Manage_Server.Application.Helpers;
using Tech_Manage_Server.Domain.Interface;
using Tech_Manage_Server.Domain.Models;

namespace Tech_Manage_Server.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImagesController(IImageRepository imageRepository, IMapper mapper) 
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadImage([FromForm] IFormFile file)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid) {
                // file upload
                var upImage = new ImageSource
                {
                    FileExtention = Path.GetExtension(file.FileName).ToLower(),
                    FileName = Guid.NewGuid().ToString(),
                    DateCreated = GetVnTime.GetVietnamTime()
                };

                upImage = await _imageRepository.Upload(file, upImage);

                var response = _mapper.Map<ImageSourceDto>(upImage);
                return Ok(response);
            }          
            return BadRequest();
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtenstions = new string[] { ".jpg", ".jpeg", ".png", ".webp", ".gif"};

            if(!allowedExtenstions.Contains(Path.GetExtension(file.FileName).ToLower())) {
                ModelState.AddModelError("file", "Không hỗ trợ định dạng file này");
            }

            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "Kích thước file phải nhỏ hơn 10MB");
            }
        }
    }
}
