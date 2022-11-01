namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message {
        
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string content { get; set; }
        
        [Required]
        [Display(Name = "Sender")]
        public string SenderId { get; set; }
        [ForeignKey("UserId")]
        public Tenant Sender { get; set; } 
        
        [Required]
        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

    }
}
