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
        [Key]
        [Required(ErrorMessage = "O Modelo deve ser informado")]
        public string Modelo { get; set; }

        
        public DateTime DataInicial { get; set; }

        
        public DateTime DataFinal{ get; set; }

        
        public int Codigo { get; set; }

        
        public string TipoMovimento { get; set; }

        
        public string Categoria { get; set; }

    }
}
