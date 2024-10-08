﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace teste_atak.Application.DTOs
{
    public class CustomerDTO
    {
        [IgnoreDataMember]
        public string? Id { get; set; }

        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido")]
        public string? Phone { get; set; }

        [Url(ErrorMessage = "Formato de URL inválido")]
        public string? ImageUrl { get; set; }
    }
}
