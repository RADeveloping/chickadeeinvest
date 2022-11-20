using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chickadee.Data;

using Enums;
using Models;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder builder)
    {

        var password = "ytyv)9kSBXmg";
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        List<ApplicationUser> users;
        List<IdentityRole> roles;
        List<Company> companies;
        List<PropertyManager> propertyManagers;
        List<Property> properties;
        List<Unit> units;
        List<Tenant> tenants;
        List<Ticket> tickets;
        builder.Entity<IdentityRole>().HasData(
            (roles = SeedDefaultRoles())
        );
        builder.Entity<ApplicationUser>().HasData(
            (users = SeedDefaultUsers(password, passwordHasher))
        );
        builder.Entity<Company>().HasData(
            (companies = SeedDefaultCompanies())
        );
        builder.Entity<PropertyManager>().HasData(
            (propertyManagers = SeedDefaultPropertyManagers(password, passwordHasher, companies))
        );
        builder.Entity<Property>().HasData(
            (properties = SeedDefaultProperties())
        );
        builder.Entity<Unit>().HasData(
            (units = SeedDefaultUnits(propertyManagers, properties))
        );
        builder.Entity<Tenant>().HasData(
            (tenants = SeedDefaultTenants(password, passwordHasher, units))
        );
        builder.Entity<IdentityUserRole<string>>().HasData(
            SeedDefaultUserRoles(users, roles, propertyManagers, tenants)
        );
        builder.Entity<Ticket>().HasData(
            (tickets = SeedDefaultTickets(units, tenants))
        );
        builder.Entity<Message>().HasData(
            SeedDefaultMessages(tenants, tickets)
        );
        builder.Entity<VerificationDocument>().HasData(
            SeedDefaultVerificationDocuments(tenants)
        );
    }

    private static List<IdentityRole> SeedDefaultRoles()
    {
        // Seed Default Roles
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole(Enums.Roles.SuperAdmin.ToString()),
            new IdentityRole(Enums.Roles.PropertyManager.ToString()),
            new IdentityRole(Enums.Roles.Admin.ToString()),
            new IdentityRole(Enums.Roles.Tenant.ToString())
        };
        foreach (IdentityRole role in roles)
        {
            role.NormalizedName = role.Name.ToUpper();
        }
        return roles;
    }

    private static List<ApplicationUser> SeedDefaultUsers(string password, PasswordHasher<ApplicationUser> passwordHasher)
    {
        // Seed Default Users
        List<ApplicationUser> users = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                UserName = "superadmin@chickadeeinvest.ca",
                Email = "superadmin@chickadeeinvest.ca",
                FirstName = "Matt",
                LastName = "Hardwick",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                DateOfBirth = DateTime.Today.AddYears(-30).AddMonths(-5).AddDays(-10),
            }
        };
        foreach (ApplicationUser user in users)
        {
            user.NormalizedUserName = user.UserName.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();
            user.PasswordHash = passwordHasher.HashPassword(user, password);
        }
        return users;
    }

    private static List<Company> SeedDefaultCompanies()
    {
        // Seed Default Companies
        List<Company> companies = new List<Company>
        {
            new Company
            {
                Name = "Company One",
                Address = "123 Main St",
                Phone = "604-235-7890",
                Email = "main@companyOne.com",
            },
            new Company
            {
                Name = "Company Two",
                Address = "Wall street",
                Phone = "778-334-4594",
                Email = "main@companyTwo.com",
            }
        };
        return companies;
    }

    private static List<PropertyManager> SeedDefaultPropertyManagers(string password, PasswordHasher<ApplicationUser> passwordHasher, List<Company> companies)
    {
        List<PropertyManager> propertyManagers = new List<PropertyManager>();
        PropertyManager propertyManagerOne = new PropertyManager()
        {
            UserName = "propertymanager@gmail.com",
            Email = "propertymanager@gmail.com",
            FirstName = "Property",
            LastName = "Manager",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            DateOfBirth = DateTime.Today.AddYears(-30).AddMonths(-5).AddDays(-10),
            CompanyId = companies[0].CompanyId

        };
        propertyManagerOne.NormalizedUserName = propertyManagerOne.UserName.ToUpper();
        propertyManagerOne.NormalizedEmail = propertyManagerOne.Email.ToUpper();
        propertyManagerOne.PasswordHash = passwordHasher.HashPassword(propertyManagerOne, password);

        PropertyManager propertyManagerTwo = new PropertyManager()
        {
            UserName = "propertymanager2@gmail.com",
            Email = "propertymanager2@gmail.com",
            FirstName = "Manager",
            LastName = "Property",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            DateOfBirth = DateTime.Today.AddYears(-30).AddMonths(-3).AddDays(-12),
            CompanyId = companies[1].CompanyId
        };
        propertyManagerTwo.NormalizedUserName = propertyManagerTwo.UserName.ToUpper();
        propertyManagerTwo.NormalizedEmail = propertyManagerTwo.Email.ToUpper();
        propertyManagerTwo.PasswordHash = passwordHasher.HashPassword(propertyManagerTwo, password);

        propertyManagers.Add(propertyManagerOne);
        propertyManagers.Add(propertyManagerTwo);

        return propertyManagers;
    }

    private static List<Property> SeedDefaultProperties()
    {
        return new List<Property>() {
            new Property() {
                Name = "The Evergreen Managed By PM 1",
                Address = "742 Evergreen Terrace",
            },
            new Property() {
                Name = "Montana Apartments Managed By PM 2",
                Address = "123 Sesame Street",
            },
            new Property() {
                Name = "Arcola Managed by PM 2",
                Address = "7488 Hazel Street",
            },
        };
    }

    private static List<Unit> SeedDefaultUnits(List<PropertyManager> propertyManagers, List<Property> properties)
    {
        return new List<Unit>() {
            new Unit() {
                UnitNo = 100,
                UnitType = UnitType.Studio,
                PropertyId = properties[0].PropertyId,
                PropertyManagerId = propertyManagers[0].Id,
            },
            new Unit() {
                UnitNo = 200,
                UnitType = UnitType.OneBedroom,
                PropertyId = properties[1].PropertyId,
                PropertyManagerId = propertyManagers[1].Id,
            },
            new Unit() {
                UnitNo = 300,
                UnitType = UnitType.OneBedroom,
                PropertyId = properties[1].PropertyId,
            },
        };
    }

    private static List<Tenant> SeedDefaultTenants(string password, PasswordHasher<ApplicationUser> passwordHasher, List<Unit> units)
    {
        List<Tenant> tenants = new List<Tenant>();
        Tenant tenantOne = new Tenant()
        {
            UserName = "tenant@gmail.com",
            Email = "tenant@gmail.com",
            FirstName = "Tenant",
            LastName = "User",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            DateOfBirth = DateTime.Today.AddYears(-20).AddMonths(-5).AddDays(-10),
            UnitId = units[0].UnitId,

        };
        tenantOne.NormalizedUserName = tenantOne.UserName.ToUpper();
        tenantOne.NormalizedEmail = tenantOne.Email.ToUpper();
        tenantOne.PasswordHash = passwordHasher.HashPassword(tenantOne, password);

        Tenant tenantTwo = new Tenant()
        {
            UserName = "tenant2@gmail.com",
            Email = "tenant2@gmail.com",
            FirstName = "User",
            LastName = "Tenant",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            DateOfBirth = DateTime.Today.AddYears(-20).AddMonths(-5).AddDays(-10),
            UnitId = units[1].UnitId,


        };
        tenantTwo.NormalizedUserName = tenantTwo.UserName.ToUpper();
        tenantTwo.NormalizedEmail = tenantTwo.Email.ToUpper();
        tenantTwo.PasswordHash = passwordHasher.HashPassword(tenantTwo, password);
        tenants.Add(tenantOne);
        tenants.Add(tenantTwo);
        return tenants;
    }

    private static List<IdentityUserRole<string>> SeedDefaultUserRoles(List<ApplicationUser> users, List<IdentityRole> roles, List<PropertyManager> propertyManagers, List<Tenant> tenants)
    {
        // Seed Default User Roles
        List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.Tenant.ToString()).Id
            },
            new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.PropertyManager.ToString()).Id
            },
            new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.Admin.ToString()).Id
            },
            new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.SuperAdmin.ToString()).Id
            },
            new IdentityUserRole<string>
            {
                UserId = propertyManagers[0].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.PropertyManager.ToString()).Id
            },
            new IdentityUserRole<string>
            {
                UserId = propertyManagers[1].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.PropertyManager.ToString()).Id
            },
            new IdentityUserRole<string>
            {
                UserId = tenants[0].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.Tenant.ToString()).Id
            },
            new IdentityUserRole<string>
            {
                UserId = tenants[1].Id,
                RoleId = roles.First(q => q.Name == Enums.Roles.Tenant.ToString()).Id
            }
        };
        return userRoles;
    }

    private static List<Ticket> SeedDefaultTickets(List<Unit> units, List<Tenant> tenants)
    {
        return new List<Ticket>() {
                new Ticket() {
                    TicketId = 1,
                    Problem = "Massive Leakage",
                    Description = "Massive Leak from the Kitchen pipe",
                    CreatedOn = new DateTime(2022, 10, 3),
                    EstimatedDate = new DateTime(2022, 11, 3),
                    Status = Enums.TicketStatus.Open,
                    Severity = Enums.TicketSeverity.High,
                    UnitId = units[0].UnitId,
                    CreatedById = tenants[0].Id
                },
                new Ticket() {
                    TicketId = 2,
                    CreatedOn = new DateTime(2021, 10, 3),
                    EstimatedDate = new DateTime(2022, 10, 3),
                    Problem = "Earthquake repair",
                    Description = "Need repairing the floors from last earthquake",
                    Status = Enums.TicketStatus.Open,
                    Severity = Enums.TicketSeverity.Medium,
                    UnitId = units[0].UnitId,
                    CreatedById = tenants[0].Id
                },
                new Ticket() {
                    TicketId = 3,
                    CreatedOn = new DateTime(1999, 10, 3),
                    EstimatedDate = new DateTime(2000, 11, 3),
                    ClosedDate = new DateTime(2000, 11, 11),
                    Problem = "Tornado damage",
                    Description = "Need to fix the roof that was damaged by the tornado",
                    Status = Enums.TicketStatus.Closed,
                    Severity = Enums.TicketSeverity.High,
                    UnitId = units[1].UnitId,
                    CreatedById = tenants[1].Id
                },
                new Ticket() {
                    TicketId = 4,
                    CreatedOn = new DateTime(2019, 10, 3),
                    EstimatedDate = new DateTime(2022, 11, 3),
                    Problem = "Rat infestation",
                    Description = "Currently getting by with rat traps",
                    Status = Enums.TicketStatus.Open,
                    Severity = Enums.TicketSeverity.Medium,
                    UnitId = units[1].UnitId,
                    CreatedById = tenants[1].Id
                },
        };
    }

    private static List<Message> SeedDefaultMessages(List<Tenant> tenants, List<Ticket> tickets)
    {
        return new List<Message>() {
            new Message(){
                Content = "This is a message",
                SenderId = tenants[0].Id,
                CreatedDate = DateTime.Now.AddHours(-23),
                TicketId = tickets[0].TicketId,
            },
        };
    }

    private static List<VerificationDocument> SeedDefaultVerificationDocuments(List<Tenant> tenants)
    {
        return new List<VerificationDocument>() {
            new VerificationDocument() {
                Data = Array.Empty<byte>(),
                DocumentType = Enums.DocumentType.PhotoIdentification,
                TenantId = tenants[0].Id
            },
        };
    }
}