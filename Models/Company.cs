namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Company {
        [Key]
        public String CompanyId { get; set; } = Guid.NewGuid().ToString();
        
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
        
        public ICollection<PropertyManager>? PropertyManagers { get; set; } = new List<PropertyManager>(); // Many to Many Relationship between ticket and ticket image

        
    }
}
