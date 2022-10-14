

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chickadee.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        public int UnitNo { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<ApplicationUser> Tenants { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
        public int PropertyId { get; set; }
    }
}