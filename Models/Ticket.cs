using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using chickadee.Enums;

namespace chickadee.Models
{
    public class Ticket
    {
       [Key] 
       public int TicketId { get; set; } 

       [Required]
       [Display(Name = "Problem")]
       public string Problem { get; set; }
       
       [Required]
       [Display(Name = "Description")]
       public string Description { get; set; }
       
       [Required] 
       [Display(Name = "Created On")] 
       [DataType(DataType.DateTime)]
       public DateTime CreatedOn { get; set; }
       
       [Display(Name = "Estimated Completion Date")] 
       [DataType(DataType.DateTime)]
       public DateTime? EstimatedDate { get; set; }
 
       [Display(Name = "Status")] 
       public TicketStatus Status { get; set; }
       
       [Display(Name = "Severity")] 

       public TicketSeverity Severity { get; set; }
       
       [Display(Name = "Closed Date")] 
       [DataType(DataType.DateTime)]
       public DateTime? ClosedDate { get; set; }

       
       public string? UnitId { get; set; }
       [ForeignKey("UnitId")]
       public Unit? Unit { get; set; }
       
       public string? CreatedById { get; set; } // OWNER OF TICKET. Anyone can create ticket
       [ForeignKey("CreatedById")]
       public ApplicationUser? CreatedBy { get; set; }
       
        
        // public string ClosedById { get; set; }
        // [ForeignKey("ClosedById")]
        // public virtual ApplicationUser ClosedBy { get; set; }
       
       public ICollection<Message>? Messages { get; set; } = new List<Message>(); 
       public ICollection<TicketImage>? Images { get; set; } = new List<TicketImage>(); // Many to Many Relationship between ticket and ticket image
    }
}
