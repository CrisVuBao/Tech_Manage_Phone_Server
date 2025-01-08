using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_Manage_Server.Application.Helpers
{
    public class CustomUserIdProvider: IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("sub")?.Value;// lấy Id người dùng từ JWT
        }
    }
}
