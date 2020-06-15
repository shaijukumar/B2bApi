using API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.UserDetails
{
    public class UserCommonFunctions
    {
        public static async Task AddRoles(IServiceProvider serviceProvider, string role, string userEmail)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            AppUser user = await UserManager.FindByEmailAsync(userEmail);
            var User = new IdentityUser();
            await UserManager.AddToRoleAsync(user, role);
        }
    }
}
