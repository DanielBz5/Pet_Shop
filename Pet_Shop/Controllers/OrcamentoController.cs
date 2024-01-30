using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Controllers
{
    public class OrcamentoController : Controller
    {
        OrcamentoDao orcamentodao;
        public OrcamentoController()
        {
            orcamentodao = new OrcamentoDao();
        }


        public IActionResult Orcamento()
        {
            try
            {
                List<Servicos> servicos = orcamentodao.ListaServicos();
                List<string> ListaOrcamente = servicos.Select(servico => servico.Nome).ToList();
                List<int> ListaValorOrcamento = servicos.Select(servico => servico.Valor).ToList();
                ViewBag.ListaOrcamente = ListaOrcamente;
                ViewBag.ListaValorOrcamento = ListaValorOrcamento;
            }
            catch// esse try catch é para carregar mesmo se der erro no banco de dados
            {
                List<Servicos> servicos = new List<Servicos>();
                Servicos servico = new Servicos
                {
                    Cod = 0,
                    Nome = "Erro no Banco",
                    Valor = 0,
                    Descricao = "Erro no Banco"
                };

                servicos.Add(servico);

                List<string> ListaOrcamente = servicos.Select(servico => servico.Nome).ToList();
                List<int> ListaValorOrcamento = servicos.Select(servico => servico.Valor).ToList();
                ViewBag.ListaOrcamente = ListaOrcamente;
                ViewBag.ListaValorOrcamento = ListaValorOrcamento;
            } 

            return View();
        }


        public IActionResult OrcamentoSucesso()
        {
            return View();
        }
    }
}
