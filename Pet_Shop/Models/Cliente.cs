using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Cliente
    {
        [Key]
        [Required(ErrorMessage ="O CPF deve ser informado")]
        [StringLength(11,MinimumLength =11, ErrorMessage ="O CPF deve ter 11 caracteres")]
        public string Cpf { get; set; }


        [Required(ErrorMessage = "O Nome deve ser informado")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O nome deve ter de 4 à 50 caracteres")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "A Senha deve ser informado")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "A Senha deve ter de 3 à 20 caracteres")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "O Telefone deve ser informado")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "O Telefone deve ter de 8 à 12 caracteres")]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "O Endereço deve ser informado")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O Endereço deve ter de 5 à 50 caracteres")]
        public string Endereco { get; set; }


        public bool RememberMe { get; set; }
    }
}
