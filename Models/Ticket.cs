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
       public DateTime CreatedOn { get; set; }
       public DateTime EstimatedDate { get; set; }
       public string Problem { get; set; }
       public string Description { get; set; }
       public TicketStatus Status { get; set; }
       public TicketSeverity Severity { get; set; }
       [ForeignKey("UnitId")]
       public Unit Unit { get; set; }
       public int UnitId { get; set; }
    }
}