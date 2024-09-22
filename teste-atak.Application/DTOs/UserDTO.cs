using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace teste_atak.Application.DTOs
{
    public class UserDTO
    {
        [IgnoreDataMember]
        public string? Id { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string? Email { get; set; }

        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string? Name { get; set; }

        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        public string? PasswordHash { get; set; }

        [Url(ErrorMessage = "Formato de URL inválido")]
        public string? AvatarUrl { get; set; }

        public bool IsEmailVerified { get; set; } = false;
    }
}
