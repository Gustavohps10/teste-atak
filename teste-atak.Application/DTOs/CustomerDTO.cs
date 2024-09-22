using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace teste_atak.Application.DTOs
{
    public class CustomerDTO
    {
        [IgnoreDataMember]
        public string? Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Formato de telefone inválido")]
        public required string Phone { get; set; }

        [Url(ErrorMessage = "Formato de URL inválido")]
        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
