namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    public class Tenant  : ApplicationUser{
        
        [Display(Name = "Lease Number")]
        public string? LeaseNumber { get; set;} // Lease can be A00864777 or 1234567890 therefore string is used.
        
        [Display(Name = "Unit ID")]
        public int? UnitId { get; set;}
        [ForeignKey("UnitId")]
        public Unit? Unit { get; set; }
        
        public List<VerificationDocument>? VerificationDocuments { get; set; }

    }
}
