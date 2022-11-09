namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using Microsoft.AspNetCore.Mvc;
    
    public class VerificationDocument {
        [Key]
        public string VerificationDocumentId { get; set; } = Guid.NewGuid().ToString();
        
        public byte[]? Data { get; set; }
        
        [Required]
        public DocumentType DocumentType { get; set; }
        
        public string? ResponseMessage { get; set; }
        
        [ForeignKey("UserId")]
        public Tenant? Tenant { get; set; }
        public string TenantId { get; set; }
    }
}
