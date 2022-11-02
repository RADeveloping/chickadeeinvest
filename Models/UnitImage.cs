namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UnitImage {
        [Key]
        public String UnitImageId { get; set; }
        
        [Required]
        public byte[] data { get; set; }
        
        [Required]
        public DateTime UploadDate { get; set; }
        
        public String UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
    }
}
