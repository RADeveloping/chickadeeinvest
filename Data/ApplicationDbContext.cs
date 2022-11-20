using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using chickadee.Models;
using chickadee.Settings;

namespace chickadee.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    private readonly UserSettings userSettings;

    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions, IOptions<UserSettings> userSettings)
        : base(options, operationalStoreOptions)
    {
        this.userSettings = userSettings.Value;
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
    public DbSet<Ticket>? Tickets { get; set; }
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
        
        builder.Entity<Ticket>()
            .Property(r => r.TicketId)
            .ValueGeneratedOnAdd();
        
        builder.Entity<Ticket>()
            .Property(s => s.CreatedOn)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<Message>()
            .Property(s => s.CreatedDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<TicketImage>()
            .Property(t => t.UploadDate)
            .HasDefaultValueSql("GETDATE()");
        
        builder.Entity<UnitImage>()
            .Property(t => t.UploadDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<UnitNote>()
            .Property(t => t.UploadDate)
            .HasDefaultValueSql("GETDATE()");

        builder.Seed(this.userSettings);

    }

    

    
    

}