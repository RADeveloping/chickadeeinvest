using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace chickadee.Models {

    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser {
        
        [Required]
        [PersonalData]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [PersonalData]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public int UsernameChangeLimit { get; set; } = 10;

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [PersonalData]
        [Display(Name = "Profile Picture")]
        public byte[]? ProfilePicture { get; set; }
        
        [Display(Name = "Unit ID")]
        public string? UnitId { get; set;}
        [ForeignKey("UnitId")]
        
        [NotMapped]
        public string FullName => this.FirstName + " " + this.LastName;


        public ICollection<Message>? Messages { get; set; } = new List<Message>();
        
        public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
        
        public ICollection<TicketImage>? TicketImage { get; set; } = new List<TicketImage>();

        
    }
}