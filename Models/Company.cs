namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        [Key]
        public int Id { get; set; } 
        
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        public byte[]? Logo { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
    }
}
