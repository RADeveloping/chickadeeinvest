namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TicketImage {
        [Key]
        public string TicketImageId { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public byte[] data { get; set; }
        
        [Required]
        [Display(Name = "Upload Date")]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; } = DateTime.Now;
        
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket? Ticket { get; set; }
        
        [Required]
        public string CreatedById { get; set; } 
        [ForeignKey("CreatedById")]
        public ApplicationUser? CreatedBy { get; set; }
        
    }
}
