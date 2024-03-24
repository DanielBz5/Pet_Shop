using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Pet_Shop.Controllers
{
    public class AgendamentoController : Controller
    {
        AgendamentoDao agendamentodao;
        private readonly IMemoryCache _memoryCache;

        [ActivatorUtilitiesConstructor]
        public AgendamentoController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            agendamentodao = new AgendamentoDao();
        }

        public AgendamentoController()
        {
            agendamentodao = new AgendamentoDao();
        }

        [Authorize]
        public IActionResult Agendamento()
        {
            Cliente cliente = new Cliente();
            cliente.Nome = User.Identity.Name;
            cliente.Senha = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(cliente.Nome == "Admin")
            {
                return RedirectToAction("Agenda");
            }

            _memoryCache.TryGetValue("MinhaListaAgenda", out List<Agendamento> ListaAgenda);
            if (ListaAgenda == null)
            {
                ListaAgenda = agendamentodao.AgendaCliente(cliente);
            }
                
            ViewBag.ListaAgenda = ListaAgenda;
            _memoryCache.Remove("MinhaListaAgenda");

            List<Servicos> servicos = agendamentodao.ListaServicos();
            SelectList listaSuspensa = new SelectList(servicos, "Cod", "Nome");
            ViewBag.ServicosList = listaSuspensa;

            return View();
        }

        [HttpGet("Agendamento/BuscaServicos")]
        public IActionResult BuscaServicos(int CodServico)
        {
            Servicos servico = agendamentodao.BuscaServico(CodServico);
            if (servico == null)
            {
                return NotFound();
            }
            return Ok(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agendamento(Servicos servicos, Agendamento agendamento)
        {
            Pet pet = new Pet();
            Cliente cliente = new Cliente();
            cliente.Nome = User.Identity.Name;
            cliente.Senha = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (User.Identity.IsAuthenticated && cliente != null)
            {
                var result = agendamentodao.BuscaClientePet(cliente);
                cliente = result.Item1;
                pet = result.Item2;

            }
            else
            {
                return View("Error");
            }

            if(cliente == null && pet == null )
            {
                return View("Error");
            }
            else 
            {
                var inclui = agendamentodao.IncluiAgendamento(cliente, pet, servicos, agendamento);
                if (inclui ==  true)
                {
                    return View("AgendamentoSucesso");
                }
                else
                {
                    return View("Error");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgendaCliente(Agendamento agendamento)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = User.Identity.Name;
            cliente.Senha = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cliente = agendamentodao.BuscaCliente(cliente);
            cliente.Cpf = agendamento.Cpf;

            string Data = agendamento.Data.ToString("dd/MM/yyyy");

            if (Data != "01/01/0001" || agendamento.Cpf != null)
            {
                List<Agendamento> ListaAgenda = agendamentodao.FiltraAgenda(agendamento);
                _memoryCache.Set("MinhaListaAgenda", ListaAgenda);

                return RedirectToAction("Agendamento");
            }
            else
            {
                List<Agendamento> ListaAgenda = agendamentodao.AgendaCliente(cliente);
                _memoryCache.Set("MinhaListaAgenda", ListaAgenda);

                return RedirectToAction("Agendamento");
            }

        }


        [Authorize]
        public IActionResult Agenda()
        {
            List<Agendamento> ListaAgenda = agendamentodao.ListaAgenda();

            return View(ListaAgenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agenda(Agendamento agendamento)
        {
            string Data = agendamento.Data.ToString("dd/MM/yyyy");

            if (Data != "01/01/0001" || agendamento.Cpf != null)
            {
                List<Agendamento> ListaAgenda = agendamentodao.FiltraAgenda(agendamento);
                return View("Agenda",ListaAgenda);
            }
            else
            {
                List<Agendamento> ListaAgenda = agendamentodao.ListaAgenda();

                return View(ListaAgenda);
            }

        }

        [Authorize]
        public IActionResult DeleteAgendamento(int Cod)
        {
            Agendamento agendamento = new Agendamento();
            agendamento.Cod = Cod;
            agendamento = agendamentodao.BuscaAgendamento(agendamento);

            return View(agendamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAgendamento(Agendamento agendamento)
        {
            bool result = agendamentodao.DeleteAgendamento(agendamento);
            if (result == true)
            {
                return RedirectToAction("Agenda");
            }
            else
            {
                return View("Error");
            }
            
        }


    }
}
