using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Produto
    {
        [Key]
        public int Cod { get; set; }

        [Required(ErrorMessage = "O Nome deve ser informado")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve ter de 3 à 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Valor deve ser informado")]
        [Range(0, int.MaxValue, ErrorMessage = "O valor deve ser igual ou maior que 0")]
        public double Valor { get; set; }


        [Required(ErrorMessage = "A Quantidade deve ser informado")]
        [Range(0, int.MaxValue, ErrorMessage = "A Quantidade deve ser igual ou maior que 0")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O Estoque Minimo deve ser informado")]
        [Range(0, int.MaxValue, ErrorMessage = "O Estoque Minimo deve ser igual ou maior que 0")]
        public int Estoque_Minimo { get; set; }

        [Required(ErrorMessage = "A Categoria deve ser informado")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "A Descrição deve ser informada")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O Estoque Minimo deve ter de 5 à 50 caracteres")]
        public string Descricao { get; set; }

        [BindNever]
        public byte[] Imagem{ get; set; }
    }
}
