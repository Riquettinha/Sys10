using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sys10.Data.Models
{
    public class User : ModelBase
    {
        public User() : base("User", "Id")
        {
        }

        public Guid Id { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Password { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public bool Status { get; set; }

        public string AuthenticationToken { get; set; }
        public DateTime? AuthenticationTokenExpiration { get; set; }
    }
}
