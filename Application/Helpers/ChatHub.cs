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
            // lấy user gửi từ claims (sub = user id)
            var senderId = Context.User?.FindFirst("sub")?.Value;
            if (senderId == null) return;

            var sender = await _userManager.FindByIdAsync(senderId);

            // kiểm tra reciver (người nhận)
            var receiver = await _userManager.FindByIdAsync(receiverId.ToString());
            if(receiver == null) return;

            // kiểm tra role
            var senderRoles = await _userManager.GetRolesAsync(sender);
            var receiverRoles = await _userManager.GetRolesAsync(receiver);

            // User chỉ được chat với Admin
            if (senderRoles.Contains("Member") && !receiverRoles.Contains("Admin") && !receiverRoles.Contains("Employee")) return;

            // save database
            var message = new Message
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Content = content,
                SentAt = GetVnTime.GetVietnamTime(),
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // gửi tin nhắn đến receiver(người nhận) với userId
            await Clients.Users(receiverId.ToString())
                .SendAsync("ReceiveMessage", sender.Id, content, GetVnTime.GetVietnamTime());

            // có thể gửi cho chính sender để cập nhật ui
            await Clients.Caller.SendAsync("MessageSentConfirmation", message.MessageId);

        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

             await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await base.OnConnectedAsync();
        }
    }
}
