using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Estoque
    {
        [Key]
        public int Cod_Movimento { get; set; }

        public int Cod_Produto { get; set; }

        public string Nome { get; set; }

        public string Tipo_Movimento { get; set; }

        public DateTime Data_ { get; set; }

        public int Quantidade { get; set; }

    }
}
