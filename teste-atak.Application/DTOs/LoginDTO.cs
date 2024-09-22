using System.ComponentModel.DataAnnotations;

namespace teste_atak.Application.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public required string Password { get; set; }
    }
}
