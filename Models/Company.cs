namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        [Key]
        public int CompanyId { get; set; } 
        
        public string Name { get; set; }
        
        public byte[]? Logo { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        
    }
}
