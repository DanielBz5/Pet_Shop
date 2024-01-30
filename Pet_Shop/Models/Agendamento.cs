using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Models
{
    public class Agendamento
    {
        public int Cod { get; set; }

        [Required(ErrorMessage = "O CPF deve ser informado")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        public string Cpf { get; set; }


        [Required(ErrorMessage = "O Nome deve ser informado")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O nome deve ter de 4 à 50 caracteres")]
        public string Nome_Cliente { get; set; }


        [Required(ErrorMessage = "O Telefone deve ser informado")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "O Telefone deve ter de 8 à 12 caracteres")]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "O Nome deve ser informado")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O nome deve ter de 4 à 50 caracteres")]
        public string Nome_Pet { get; set; }


        [Required(ErrorMessage = "A Espécia deve ser informado")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "A Espécia deve ter de 3 à 20 caracteres")]
        public string Especie { get; set; }


        [Required(ErrorMessage = "O Serviço deve ser informado")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter de 3 à 50 caracteres")]
        public string Nome_Servico { get; set; }


        [Required(ErrorMessage = "A Data deve ser informado")]
        public DateTime Data { get; set; }


        public override string ToString()
        {
            return $"Cod: {Cod}, CPF: {Cpf}, Nome_Cliente: {Nome_Cliente}, Telefone: {Telefone}, Nome_Pet: {Nome_Pet}, Especie: {Especie}, Nome_Servico: {Nome_Servico}, Data: {Data}, ";
        }
    }
}
