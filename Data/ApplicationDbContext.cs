using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using chickadee.Models;

namespace chickadee.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
       

    }

    
    public DbSet<ApplicationUser> User { get; set; }
    public DbSet<chickadee.Models.Company>? Company { get; set; }
    public DbSet<chickadee.Models.Message>? Messages { get; set; }
    public DbSet<Property>? Property { get; set; }
    public DbSet<PropertyManager>? PropertyManagers { get; set; }
    public DbSet<Unit>? Unit { get; set; }
    public DbSet<UnitImage>? UnitImage { get; set; }
    public DbSet<UnitNote>? UnitNote { get; set; }
    public DbSet<Tenant>? Tenant { get; set; }
    public DbSet<Ticket>? Ticket { get; set; }
    public DbSet<TicketImage>? TicketImage { get; set; }

    public DbSet<VerificationDocument>? VerificationDocuments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // disable cascade delete
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        
        
        builder.Seed();

    }

    

    
    

}