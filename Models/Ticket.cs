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

       
       public int UnitId { get; set; }
       [ForeignKey("UnitId")]
       public Unit Unit { get; set; }
       
       [ForeignKey("UserId")]
       public String UserId { get; set; } // OWNER OF TICKET. Anyone can create ticket
        public ApplicationUser User { get; set; }
       
        //
        // public String ClosedById { get; set; }
        // [ForeignKey("ClosedById")]
        // public ApplicationUser? ClosedByUser { get; set; }
       
       public List<Message>? Messages { get; set; }
       public ICollection<TicketImage>? Images { get; set; } // Many to Many Relationship between ticket and ticket image
    }
}
