using Microsoft.AspNetCore.Identity;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Data
{
    public class DbInitializer
    {
        public static async Task Inittialize(UserManager<ApplicationUser> userManager)
        {
            //if (!userManager.Users.Any())
            //{
            //    var admin = new ApplicationUser
            //    {
            //        UserName = "admin@gmail.com",
            //        Email = "admin@gmail.com",
            //        FullName = "admin",
            //        Avatar = "/images/avatar/admin.webp"
            //    };
            //    await userManager.CreateAsync(admin, "Pa$$w0d");
            //    await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin", "Employee" });

            //    var employee = new ApplicationUser
            //    {
            //        UserName = "employ@gmail.com",
            //        Email = "employ@gmail.com",
            //        FullName = "employee",
            //        Avatar = "/images/avatar/admin.webp"
            //    };
            //    await userManager.CreateAsync(employee, "Pa$$w0d");
            //    await userManager.AddToRolesAsync(employee, new[] { "Employee" });

            //    var member = new ApplicationUser
            //    {
            //        UserName = "test@gmail.com",
            //        Email = "test@gmail.com",   
            //        FullName = "Test",
            //        Avatar = "/images/avatar/test.png"
            //    };
            //    await userManager.CreateAsync(member, "Pa$$w0d");
            //    await userManager.AddToRoleAsync(member, "Member");
            //};
        }
    }
}
