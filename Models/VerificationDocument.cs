namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using Microsoft.AspNetCore.Mvc;
    
    public class VerificationDocument {
        [Key]
        public String VerificationDocumentId { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public byte[] data { get; set; }
        
        [Required]
        public DocumentType DocumentType { get; set; }
        
        public string? ResponseMessage { get; set; }
        
        [ForeignKey("UserId")]
        public Tenant? Tenant { get; set; }
        public String TenantId { get; set; }
    }
}
