namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UnitNote {
        [Key]
        public String UnitNoteId { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        [Display(Name = "Upload Date")]
        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }
        
        [ForeignKey("UnitId")]
        public Unit? Unit { get; set; }
        public String UnitId { get; set; }
    }
}
