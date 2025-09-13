using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Constant
{
    public class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Tạo roles nếu chưa tồn tại
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await roleManager.RoleExistsAsync(UserRoles.Customer))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));

            // Tạo tài khoản admin mẫu nếu cần
            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin@123"); // mật khẩu mẫu
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            }
        }
    }
}
