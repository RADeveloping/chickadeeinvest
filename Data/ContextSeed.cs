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
    }
}

