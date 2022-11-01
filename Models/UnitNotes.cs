namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UnitNotes {
        [Key]
        public int UnitNotesId { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public DateTime UploadDate { get; set; }
        
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
        public int UnitId { get; set; }
    }
}
