using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class ProdutoEstoqueViewModel
    {
        [Required(ErrorMessage = "O Codigo deve ser informado")]
        public int Cod { get; set; }


        [Required(ErrorMessage = "O Nome deve ser informado")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve ter de 3 à 30 caracteres")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "A Descrição Minimo deve ser informado")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O Estoque Minimo deve ter de 5 à 50 caracteres")]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "A Quantidade deve ser informado")]
        [Range(0, int.MaxValue, ErrorMessage = "A Quantidade deve ser igual ou maior que 0")]
        public int QuantidadeAtual { get; set; }


        [Required(ErrorMessage = "O Tipo de Movimento deve ser informado")]
        public string TipoMovimento { get; set; }


        [Required(ErrorMessage = "A Quantidade deve ser informado")]
        [Range(0, int.MaxValue, ErrorMessage = "A Quantidade deve ser igual ou maior que 0")]
        public int QuantidadeMovimento { get; set; }

        public DateTime Data_ { get; set; }
    }
}
