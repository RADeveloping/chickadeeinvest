namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PropertyManager : ApplicationUser {
        
        [Display(Name = "Company ID")]
        public String? CompanyId { get; set;}
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; } = null!;
        
        public ICollection<Unit>? Units { get; set; } = new List<Unit>();

    }
}
