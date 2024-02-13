using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Relatorio
    {
        [Required(ErrorMessage = "O Modelo deve ser informado")]
        public string Modelo { get; set; }

        [BindNever]
        public DateTime DataInicial { get; set; }

        [BindNever]
        public DateTime DataFinal{ get; set; }

        [BindNever]
        public int Codigo { get; set; }

        [BindNever]
        public string TipoMovimento { get; set; }

        [BindNever]
        public string Categoria { get; set; }

    }
}
