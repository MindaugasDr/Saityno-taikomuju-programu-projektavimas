using BidToBuy.Auth.Model;
using BidToBuy.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidToBuy.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<RestUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DatabaseSeeder(UserManager<RestUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync() 
        {
            foreach (var role in RestUserRoles.All) 
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist) {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var newAdminUser = new RestUser { UserName = "admin", PhoneNumber = "860645856" };
            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, RestUserRoles.All);
                }
            }
        }
    }
}
