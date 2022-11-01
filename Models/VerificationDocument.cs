namespace chickadee.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using Microsoft.AspNetCore.Mvc;

    [Bind()]
    public class VerificationDocument {
        [Key]
        public int VerificationDocumentId { get; set; }
        
        public byte[]? data { get; set; }
        
        [Required]
        public DocumentType DocumentType { get; set; }
        
        public string? ResponseMessage { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
