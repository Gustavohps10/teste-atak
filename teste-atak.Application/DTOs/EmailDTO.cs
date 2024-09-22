using System.ComponentModel.DataAnnotations;

namespace teste_atak.Application.DTOs
{
    public class EmailDTO
    {
        [Required]
        public required string To { get; set; }

        [Required]
        public required string Subject { get; set; }

        [Required]
        public required string Body { get; set; }

        public string? AttachmentPath { get; set; }
    }
}
