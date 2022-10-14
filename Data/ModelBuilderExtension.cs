using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chickadee.Data;
using Models;

public static class ModelBuilderExtensions {
    public static void Seed(this ModelBuilder builder)
    {
        var password = "ytyv)9kSBXmg";
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        // Seed Roles
        var superAdminRole = new IdentityRole(Enums.Roles.SuperAdmin.ToString());
        superAdminRole.NormalizedName = superAdminRole.Name.ToUpper();

        var propertyManagerRole = new IdentityRole(Enums.Roles.PropertyManager.ToString());
        propertyManagerRole.NormalizedName = propertyManagerRole.Name.ToUpper();


        var adminRole = new IdentityRole(Enums.Roles.Admin.ToString());
        adminRole.NormalizedName = adminRole.Name.ToUpper();

        var tenantRole = new IdentityRole(Enums.Roles.Tenant.ToString());
        tenantRole.NormalizedName = tenantRole.Name.ToUpper();

        List<IdentityRole> roles = new List<IdentityRole>()
        {
            superAdminRole, propertyManagerRole, adminRole, tenantRole
        };

        builder.Entity<IdentityRole>().HasData(roles);

        // -----------------------------------------------------------------------------

        List<ApplicationUser> users = new List<ApplicationUser>();

        // Seed Users
        var superAdminUser = new ApplicationUser()
        {
            UserName = "superadmin@chickadeeinvest.ca",
            Email = "superadmin@chickadeeinvest.ca",
            FirstName = "Matt",
            LastName = "Hardwick",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        superAdminUser.NormalizedUserName = superAdminUser.UserName.ToUpper();
        superAdminUser.NormalizedEmail = superAdminUser.Email.ToUpper();
        superAdminUser.PasswordHash = passwordHasher.HashPassword(superAdminUser, password);

        var propertyManagerOne = new ApplicationUser()
        {
            UserName = "propertymanager@gmail.com",
            Email = "propertymanager@gmail.com",
            FirstName = "Property",
            LastName = "Manager",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        propertyManagerOne.NormalizedUserName = propertyManagerOne.UserName.ToUpper();
        propertyManagerOne.NormalizedEmail = propertyManagerOne.Email.ToUpper();
        propertyManagerOne.PasswordHash = passwordHasher.HashPassword(propertyManagerOne, password);

        var propertyManagerTwo = new ApplicationUser()
        {
            UserName = "propertymanager2@gmail.com",
            Email = "propertymanager2@gmail.com",
            FirstName = "Manager",
            LastName = "Property",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        propertyManagerTwo.NormalizedUserName = propertyManagerTwo.UserName.ToUpper();
        propertyManagerTwo.NormalizedEmail = propertyManagerTwo.Email.ToUpper();
        propertyManagerTwo.PasswordHash = passwordHasher.HashPassword(propertyManagerTwo, password);

        var tenantOne = new ApplicationUser()
        {
            UserName = "tenant@gmail.com",
            Email = "tenant@gmail.com",
            FirstName = "Tenant",
            LastName = "User",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            UnitId = 1
        };
        tenantOne.NormalizedUserName = tenantOne.UserName.ToUpper();
        tenantOne.NormalizedEmail = tenantOne.Email.ToUpper();
        tenantOne.PasswordHash = passwordHasher.HashPassword(tenantOne, password);

        var tenantTwo = new ApplicationUser()
        {
            UserName = "tenant2@gmail.com",
            Email = "tenant2@gmail.com",
            FirstName = "User",
            LastName = "Tenant",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            UnitId = 2
        };
        tenantTwo.NormalizedUserName = tenantTwo.UserName.ToUpper();
        tenantTwo.NormalizedEmail = tenantTwo.Email.ToUpper();
        tenantTwo.PasswordHash = passwordHasher.HashPassword(tenantTwo, password);

        users.Add(superAdminUser);
        users.Add(propertyManagerOne);
        users.Add(propertyManagerTwo);
        users.Add(tenantOne);
        users.Add(tenantTwo);

        builder.Entity<ApplicationUser>().HasData(users);

        ///----------------------------------------------------

        // Seed UserRoles
        List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = superAdminUser.Id, 
            RoleId = roles.First(q => q.Name == Enums.Roles.Tenant.ToString()).Id
        });
        
        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = superAdminUser.Id, 
            RoleId = roles.First(q => q.Name == Enums.Roles.PropertyManager.ToString()).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = superAdminUser.Id, 
            RoleId = roles.First(q => q.Name == Enums.Roles.Admin.ToString()).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = superAdminUser.Id, 
            RoleId = roles.First(q => q.Name == Enums.Roles.SuperAdmin.ToString()).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = propertyManagerOne.Id,
            RoleId = roles.First(q => q.Name == Enums.Roles.PropertyManager.ToString()).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = propertyManagerTwo.Id,
            RoleId = roles.First(q => q.Name == Enums.Roles.PropertyManager.ToString()).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = tenantOne.Id,
            RoleId = roles.First(q => q.Name == Enums.Roles.Tenant.ToString()).Id
        });

        userRoles.Add(new IdentityUserRole<string>
        {
            UserId = tenantTwo.Id,
            RoleId = roles.First(q => q.Name == Enums.Roles.Tenant.ToString()).Id
        });

        List<ApplicationUser> tenants = new List<ApplicationUser>() {
            tenantOne,
            tenantTwo
        };

        List<ApplicationUser> propertyManagers = new List<ApplicationUser>() {
            propertyManagerOne,
            propertyManagerTwo
        };

        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        builder.Entity<Unit>().HasData(SeedUnits());
        builder.Entity<Ticket>().HasData(SeedTickets());
        builder.Entity<Property>().HasData(SeedProperties(propertyManagers));
    }
    public static List<Unit> SeedUnits()
        {
            List<Unit> units = new List<Unit>() {
                new Unit() {
                    UnitId = 1,
                    UnitNo = 101,
                    PropertyId = 1
                },
                new Unit() {
                    UnitId = 2,
                    UnitNo = 500,
                    PropertyId = 2
                },
            };

            return units;
        }

        public static List<Property> SeedProperties(List<ApplicationUser> propertyManagers)
        {
            List<Property> properties = new List<Property>() {
                new Property() {
                    PropertyId = 1,
                    Address = "742 Evergreen Terrace",
                    PropertyManagerId = propertyManagers[0].Id
                },
                new Property() {
                    PropertyId = 2,
                    Address = "The Montana",
                    PropertyManagerId = propertyManagers[1].Id
                },
            };

            return properties;
        }

        public static List<Ticket> SeedTickets()
        {
            List<Ticket> tickets = new List<Ticket>() {
                new Ticket() {
                    TicketId = 1,
                    CreatedOn = new DateTime(2022, 10, 3),
                    EstimatedDate = new DateTime(2022, 11, 3),
                    Problem = "Massive Leakage",
                    Description = "Massive Leak from the Kitchen pipe",
                    Status = Enums.TicketStatus.Open,
                    Severity = Enums.TicketSeverity.High,
                    UnitId = 1
                },
                new Ticket() {
                    TicketId = 2,
                    CreatedOn = new DateTime(2021, 10, 3),
                    EstimatedDate = new DateTime(2022, 10, 3),
                    Problem = "Earthquake repair",
                    Description = "Need repairing the floors from last earthquake",
                    Status = Enums.TicketStatus.Open,
                    Severity = Enums.TicketSeverity.Medium,
                    UnitId = 1
                },
                new Ticket() {
                    TicketId = 3,
                    CreatedOn = new DateTime(1999, 10, 3),
                    EstimatedDate = new DateTime(2000, 11, 3),
                    Problem = "Tornado damage",
                    Description = "Need to fix the roof that was damaged by the tornado",
                    Status = Enums.TicketStatus.Closed,
                    Severity = Enums.TicketSeverity.High,
                    UnitId = 2
                },
                new Ticket() {
                    TicketId = 4,
                    CreatedOn = new DateTime(2019, 10, 3),
                    EstimatedDate = new DateTime(2022, 11, 3),
                    Problem = "Rat infestation",
                    Description = "Currently getting by with rat traps",
                    Status = Enums.TicketStatus.Open,
                    Severity = Enums.TicketSeverity.Medium,
                    UnitId = 2
                },
            };

            return tickets;
        }

}