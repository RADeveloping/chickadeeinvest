namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TicketImage {
        [Key]
        public int TicketImageId { get; set; }
        
        [Required]
        public byte[] data { get; set; }
        
        [Required]
        public DateTime UploadDate { get; set; }
        
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
        
        public string TenantId { get; set; }
        [ForeignKey("UserId")]
        public Tenant Tenant { get; set; } 
                
    }
}
