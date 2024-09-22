using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace teste_atak.Application.DTOs
{
    public class UserDTO
    {
        [IgnoreDataMember]
        public string? Id { get; set; } // Permite null ao criar

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        public required string Password { get; set; }

        [Url(ErrorMessage = "Formato de URL inválido")]
        public string? AvatarUrl { get; set; }

        public bool IsEmailVerified { get; set; }

    }

}
