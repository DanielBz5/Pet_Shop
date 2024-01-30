using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Pet
    {
        [Key]
        public string CpfDono { get; set; }

        [Required(ErrorMessage = "O Nome deve ser informado")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O nome deve ter de 4 à 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Espécia deve ser informado")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "A Espécia deve ter de 3 à 20 caracteres")]
        public string Especie { get; set; }

        [Required(ErrorMessage = "A Raça deve ser informado")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "A Raça deve ter de 3 à 20 caracteres")]
        public string Raca { get; set; }


    }
}
