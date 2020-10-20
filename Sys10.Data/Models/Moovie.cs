using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.Models
{
    public class Moovie : ModelBase
    {
        public Moovie() : base("Moovie", "Id")
        {
        }

        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Data de Lançamento")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Observações")]
        public string Observations { get; set; }

        //Foreign keys
        public Guid DirectorId { get; set; }
        public Guid CountryId { get; set; }
        public Guid GenreId { get; set; }

        //Relationships
        public virtual Artist Director { get; set; }
        public virtual Country Country { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
