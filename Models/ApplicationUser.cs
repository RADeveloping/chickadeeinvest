using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace chickadee.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int UsernameChangeLimit { get; set; } = 10;
    public byte[]? ProfilePicture { get; set; }
    [ForeignKey("UnitId")]
    public int? UnitId { get; set;}
    public Unit? Unit { get; set; }
    public List<Ticket>? Tickets { get; set; }
}
