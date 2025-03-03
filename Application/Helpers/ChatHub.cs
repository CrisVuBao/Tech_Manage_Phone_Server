using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Application.Helpers
{
    [Authorize]
    public class ChatHub: Hub
    {
        private readonly ManageDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(ManageDBContext context, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }
 
        // hàm gửi tin nhắn
        public async Task SendMessage(int receiverId, string content)
        {
            // Lấy user gửi từ claims (sub = user id)
            var senderIdStr = Context.User?.FindFirst("sub")?.Value;
            if (senderIdStr == null) return;

            if (!int.TryParse(senderIdStr, out int senderId))
            {
                // ID không hợp lệ
                await Clients.Caller.SendAsync("Error", "Invalid sender ID.");
                return;
            }

            var sender = await _userManager.FindByIdAsync(senderIdStr);
            if (sender == null)
            {
                await Clients.Caller.SendAsync("Error", "Sender not found.");
                return;
            }

            var receiver = await _userManager.FindByIdAsync(receiverId.ToString());
            if (receiver == null)
            {
                await Clients.Caller.SendAsync("Error", "Receiver not found.");
                return;
            }

            // Kiểm tra vai trò
            var senderRoles = await _userManager.GetRolesAsync(sender);
            var receiverRoles = await _userManager.GetRolesAsync(receiver);

            // Áp dụng quy tắc phân quyền
            if (senderRoles.Contains("Member"))
            {
                if (!receiverRoles.Contains("Admin") && !receiverRoles.Contains("Employee"))
                {
                    // Member chỉ được gửi tin tới Admin hoặc Employee
                    await Clients.Caller.SendAsync("Error", "Bạn chỉ có thể gửi tin nhắn tới Admin hoặc Employee.");
                    return;
                }
            }
            // Admin có thể gửi tới bất kỳ ai, không cần kiểm tra thêm

            // Lưu tin nhắn vào cơ sở dữ liệu
            var message = new Message
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Content = content,
                SentAt = GetVnTime.GetVietnamTime(), // Giả sử bạn có phương thức này để lấy giờ Việt Nam
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // Gửi tin nhắn tới receiver
            await Clients.User(receiverId.ToString())
                .SendAsync("ReceiveMessage", sender.Id, content, message.SentAt);

            // Gửi xác nhận tới sender
            await Clients.Caller.SendAsync("MessageSentConfirmation", message.MessageId);

        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }
    }
}
