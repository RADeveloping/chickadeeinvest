using System;
using chickadee.Models;
using Microsoft.AspNetCore.Identity;

namespace chickadee.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.PropertyManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Tenant.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin@chickadeeinvest.ca",
                Email = "superadmin@chickadeeinvest.ca",
                FirstName = "Matt",
                LastName = "Hardwick",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "ytyv)9kSBXmg");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Tenant.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.PropertyManager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}

