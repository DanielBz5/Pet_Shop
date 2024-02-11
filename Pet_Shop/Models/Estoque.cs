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

        [Required(ErrorMessage = "O Codigo deve ser informado")]
        public int Cod_Produto { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required(ErrorMessage = "O Tipo de Movimento deve ser informado")]
        public string Tipo_Movimento { get; set; }

        public DateTime Data_ { get; set; }

        [Required(ErrorMessage = "A Quantidade deve ser informado")]
        [Range(0, int.MaxValue, ErrorMessage = "A Quantidade deve ser igual ou maior que 0")]
        public int Quantidade { get; set; }

        

    }
}
