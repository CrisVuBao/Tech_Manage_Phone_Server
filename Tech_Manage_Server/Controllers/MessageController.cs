using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Application.Helpers;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ManageDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(ManageDBContext manageDBContext, UserManager<ApplicationUser> userManager) 
        {
            _context = manageDBContext;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("conversation/{userId}")]
        public async Task<IActionResult> GetConversation(int userId)
        {
            // Lấy user hiện tại
            //var currentUserId = User.FindFirst("name")?.Value;
            var currentUserId = User.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();

            if (!int.TryParse(currentUserId, out int senderId))
            {
                return Unauthorized();
            }

            // Kiểm tra xem người dùng có tồn tại
            var sender = await _userManager.FindByIdAsync(senderId.ToString());
            if (sender == null) return Unauthorized();

            // Lấy tin nhắn giữa senderId và userId
            var messages = await _context.Messages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == userId)
                         || (m.SenderId == userId && m.ReceiverId == senderId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return Ok(messages);
        }

        [Authorize]
        [HttpPut("mark-read/{messageId}")]
        public async Task<IActionResult> MarkAsRead(int messageId)
        {
            var currentUserId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();

            if (!int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }

            var message = await _context.Messages.FindAsync(messageId);
            if (message == null) return NotFound();

            // Chỉ cho phép receiver đánh dấu
            if (message.ReceiverId != userId)
            {
                return Forbid();
            }

            message.IsRead = true;
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();

            return Ok("Tin nhắn đã đánh dấu đọc");
        }
    }
}
