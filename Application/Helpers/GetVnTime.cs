using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_Manage_Server.Application.Helpers
{
    public static class GetVnTime
    {
        public static DateTime GetVietnamTime()
        {
            // Lấy thông tin múi giờ Việt Nam
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Lấy giờ hiện tại (UTC) và chuyển đổi sang giờ Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

            return vietnamTime;
        }
    }
}
