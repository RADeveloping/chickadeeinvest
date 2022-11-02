using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace chickadee.Data;

using Enums;
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

        List<ApplicationUser> AdminSuperAdminuserList = new List<ApplicationUser>();
        var superAdminUser = new ApplicationUser()
        {
            UserName = "superadmin@chickadeeinvest.ca",
            Email = "superadmin@chickadeeinvest.ca",
            FirstName = "Matt",
            LastName = "Hardwick",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            DateOfBirth = DateTime.Today.AddYears(-30).AddMonths(-5).AddDays(-10),
        };
        superAdminUser.NormalizedUserName = superAdminUser.UserName.ToUpper();
        superAdminUser.NormalizedEmail = superAdminUser.Email.ToUpper();
        superAdminUser.PasswordHash = passwordHasher.HashPassword(superAdminUser, password);
        AdminSuperAdminuserList.Add(superAdminUser);
        builder.Entity<ApplicationUser>().HasData(AdminSuperAdminuserList);

        // -----------------------------------------------------------------------------

        List<PropertyManager> PropertyManagerList = new List<PropertyManager>();
        
        var propertyManagerOne = new PropertyManager()
        {
            UserName = "propertymanager@gmail.com",
            Email = "propertymanager@gmail.com",
            FirstName = "Property",
            LastName = "Manager",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            DateOfBirth = DateTime.Today.AddYears(-30).AddMonths(-5).AddDays(-10),
            
        };
        propertyManagerOne.NormalizedUserName = propertyManagerOne.UserName.ToUpper();
        propertyManagerOne.NormalizedEmail = propertyManagerOne.Email.ToUpper();
        propertyManagerOne.PasswordHash = passwordHasher.HashPassword(propertyManagerOne, password);

        var propertyManagerTwo = new PropertyManager()
        {
            UserName = "propertymanager2@gmail.com",
            Email = "propertymanager2@gmail.com",
            FirstName = "Manager",
            LastName = "Property",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            DateOfBirth = DateTime.Today.AddYears(-30).AddMonths(-3).AddDays(-12),

        };
        propertyManagerTwo.NormalizedUserName = propertyManagerTwo.UserName.ToUpper();
        propertyManagerTwo.NormalizedEmail = propertyManagerTwo.Email.ToUpper();
        propertyManagerTwo.PasswordHash = passwordHasher.HashPassword(propertyManagerTwo, password);

        PropertyManagerList.Add(propertyManagerOne);
        PropertyManagerList.Add(propertyManagerTwo);
        
        builder.Entity<PropertyManager>().HasData(PropertyManagerList);
        
        // -----------------------------------------------------------------------------
        var properties = new List<Property>() {
            new Property() {
                Name = "The Evergreen",
                Address = "742 Evergreen Terrace",
            },
            new Property() {
                Name = "Montana Apartments",
                Address = "123 Sesame Street",
            },
            new Property() {
                Name = "Arcola",
                Address = "7488 Hazel Street",
            },
        };
        
        builder.Entity<Property>().HasData(properties);
        
        // -----------------------------------------------------------------------------
        
        List<Unit> units = new List<Unit>() {
            new Unit() {
                UnitNo = 101,
                UnitType = UnitType.Studio,
                PropertyId = properties[0].PropertyId,
                // PropertyManagerId = PropertyManagerList[0].Id,
                // PropertyManager = PropertyManagerList[0]
            },
            new Unit() {
                UnitNo = 500,
                UnitType = UnitType.OneBedroom,
                PropertyId = properties[1].PropertyId,
                // PropertyManagerId = PropertyManagerList[0].Id,
                // PropertyManager = PropertyManagerList[0]
            },
        };
        
        builder.Entity<Unit>().HasData(units);

        
        // -----------------------------------------------------------------------------
        
        List<Tenant> TenantsList = new List<Tenant>();
        var tenantOne = new Tenant()
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

        var tenantTwo = new Tenant()
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
        TenantsList.Add(tenantOne);
        TenantsList.Add(tenantTwo);
        builder.Entity<Tenant>().HasData(TenantsList);

        
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

        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        ///----------------------------------------------------
        
        List<Company> companies = new List<Company>() {
            new Company(){
                Name = "Company One",
                Address = "123 Main St",
                Phone = "604-235-7890",
                Email = "main@companyOne.com",
            },
        };
        
        builder.Entity<Company>().HasData(companies);
        
        
        ///----------------------------------------------------
        List<Ticket> tickets = new List<Ticket>() {
                new Ticket() {
                    TicketId = 1,
                    Problem = "Massive Leakage",
                    Description = "Massive Leak from the Kitchen pipe",
                    CreatedOn = new DateTime(2022, 10, 3),
                    EstimatedDate = new DateTime(2022, 11, 3),
                    Status = Enums.TicketStatus.Open,
                    Severity = Enums.TicketSeverity.High,
                    UnitId = units[0].UnitId,
                    CreatedById = TenantsList[0].Id
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
                    CreatedById = TenantsList[0].Id
                },
                new Ticket() {
                    TicketId = 3,
                    CreatedOn = new DateTime(1999, 10, 3),
                    EstimatedDate = new DateTime(2000, 11, 3),
                    Problem = "Tornado damage",
                    Description = "Need to fix the roof that was damaged by the tornado",
                    Status = Enums.TicketStatus.Closed,
                    Severity = Enums.TicketSeverity.High,
                    UnitId = units[1].UnitId,
                    CreatedById = TenantsList[1].Id
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
                    CreatedById = TenantsList[1].Id
                },
        };
        
        builder.Entity<Ticket>().HasData(tickets);
        
        
        ///----------------------------------------------------

        
        List<Message> messages = new List<Message>() {
            new Message(){
                content = "This is a message",
                SenderId = TenantsList[0].Id,
                CreatedDate = DateTime.Now.AddHours(-23),
                TicketId = tickets[0].TicketId,
            },
        };
        
        builder.Entity<Message>().HasData(messages);
        
        List<VerificationDocument> verificationDocuments = new List<VerificationDocument>() {
            new VerificationDocument() {
                data = Array.Empty<byte>(),
                DocumentType = Enums.DocumentType.PhotoIdentification,
                TenantId = TenantsList[0].Id
            },
        };
        
        builder.Entity<VerificationDocument>().HasData(verificationDocuments);

        
        ///----------------------------------------------------
    }

}