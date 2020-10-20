using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Data.Models
{
    public class Artist : ModelBase
    {
        public Artist() : base("Artist", "Id")
        {
            Moovies = new List<Moovie>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public Enums.Artist.Type Type { get; set; }

        //Relationships
        public virtual ICollection<Moovie> Moovies { get; set; }
    }
}
