namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message {
        
        [Key]
        public int MessageId { get; set; }

        [Required]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string content { get; set; }
        
        [Required]
        [Display(Name = "Sender")]
        public String SenderId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser Sender { get; set; } 
        
        [Required]
        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; } 

                
    }
}
