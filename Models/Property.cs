using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chickadee.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public string Address { get; set; }
        public List<Unit> Units { get; set; }

        [ForeignKey("PropertyManagerId")]
        public string PropertyManagerId { get; set; }
        public ApplicationUser PropertyManager { get; set; }
    }
}