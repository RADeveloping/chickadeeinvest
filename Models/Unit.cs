

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chickadee.Models
{
    using Enums;

    public class Unit {
        [Key]
        public string UnitId { get; set; } = Guid.NewGuid().ToString();
        public int UnitNo { get; set; }
        
        public UnitType UnitType { get; set; }
        
        public string PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }
        
        public string? PropertyManagerId { get; set; }
        [ForeignKey("PropertyManagerId")]
        public PropertyManager? PropertyManager { get; set; }
        
        public ICollection<Tenant>? Tenants { get; set; } = new List<Tenant>();
        
        public ICollection<UnitImage>? Images { get; set; } = new List<UnitImage>();
        public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
        public ICollection<UnitNote>? Notes { get; set; } = new List<UnitNote>();
        
        
    }
}