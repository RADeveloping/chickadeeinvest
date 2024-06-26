namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message {

        [Key]
        public string MessageId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        
        [Required]
        [Display(Name = "Sender")]
        public string SenderId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? Sender { get; set; } 
        
        [Required]
        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket? Ticket { get; set; } 

                
    }
}
