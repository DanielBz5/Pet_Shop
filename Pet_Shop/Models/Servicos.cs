using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Servicos
    {
        public int Cod { get; set; }


        [Required(ErrorMessage = "O Serviço deve ser informado")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter de 3 à 50 caracteres")]
        public string Nome { get; set; }

        public int Valor { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return $"Cod: {Cod}, Nome: {Nome}, Valor: {Valor}, Descricao: {Descricao}";
        }
    }
}
