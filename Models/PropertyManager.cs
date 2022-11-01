namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PropertyManager : ApplicationUser {
        
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set;}
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        public List<Unit>? Units { get; set; }

    }
}
