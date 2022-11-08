using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chickadee.Models
{
    public class Property {
        public string PropertyId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Address { get; set; }
        
        public ICollection<Unit>? Units { get; set; } = new List<Unit>();
        
        public IQueryable<object> CreatePropertyDto(IQueryable<Property> property)
        {
            return property.Select(x => new
            {
                model = new Property()
                {
                    PropertyId = x.PropertyId,
                    Name = x.Name,
                    Address = x.Address,
                }
            });
        }

    }
    
}