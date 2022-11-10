namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Company {
        [Key]
        public string CompanyId { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [Display(Name = "Name")] 
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Logo")] 
        public byte[]? Logo { get; set; }
        
        [Display(Name = "Address")] 
        public string Address { get; set; }
        
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
