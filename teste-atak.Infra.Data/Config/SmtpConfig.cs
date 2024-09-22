using System.ComponentModel.DataAnnotations;

namespace teste_atak.Infra.Data.Config
{
    public class SmtpConfig
    {
        [Required(ErrorMessage = "O Host é obrigatório.")]
        public string Host { get; set; } = null!;

        [Required(ErrorMessage = "A Port é obrigatória.")]
        public int Port { get; set; }

        [Required(ErrorMessage = "O Username é obrigatório.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "O Password é obrigatório.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "O From é obrigatório.")]
        public string From { get; set; } = null!;
    }
}
