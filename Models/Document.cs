namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Document {
        [Key]
        public string DocumentId { get; set; }
        
        [Display(Name = "Photo ID")]
        public byte[] IdPhoto { get; set; }

        [Display(Name = "Lease Agreement")]
        public byte[] LeasePhoto { get; set; }
        
        public bool IsIdVerified { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
