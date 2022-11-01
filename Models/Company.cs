namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Company {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "Company Name")] 
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Company Logo")] 
        public byte[]? ProfilePicture { get; set; }
        
        [Display(Name = "Company Address")] 
        public string Address { get; set; }
        
        [Display(Name = "Company Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        
        [Display(Name = "Company Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
