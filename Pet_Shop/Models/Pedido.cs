using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Pedido
    {
        [Key]
        public int Cod { get; set; }

        public long? IdPagamento { get; set; }

        public string Cpf { get; set; }

        public string Nome{ get; set; }

        [Required(ErrorMessage = "O Telefone deve ser informado")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "O Telefone deve ter de 8 à 12 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Endereço deve ser informada")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Endereço deve ter de 5 à 100 caracteres")]
        public string Endereco { get; set; }

        public double ValorTotal { get; set; }

        [Required(ErrorMessage = "O Tipo de Pagamento deve ser informada")]
        public string TipoPagamento { get; set; }

        [BindNever]
        public string StatusPagamento { get; set; }

        [BindNever]
        public byte[] QrCode { get; set; }

        [BindNever]
        public string ChavePagamento { get; set; }
    }
}
