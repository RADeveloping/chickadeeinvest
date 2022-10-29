using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chickadee.Data;

using System.Web;
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
        builder.Entity<Company>().HasData(SeedCompanies());
        

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

        
        public static List<Company> SeedCompanies()
        {
            byte[] logoByte = System.Text.Encoding.UTF8.GetBytes("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");

            List<Company> companies = new List<Company>() {
                new Company() {
                    CompanyId = 1,
                    Name = "Company One",
                    Logo = logoByte,
                    PhoneNumber = "6045000098",
                    Email = "CompanyOne@gmail.com",
                },
            };

            return companies;
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