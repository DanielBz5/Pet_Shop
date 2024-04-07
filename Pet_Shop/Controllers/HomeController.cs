using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pet_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeDao homedao;

        [ActivatorUtilitiesConstructor]
        public HomeController(ILogger<HomeController> logger)
{
            homedao = new HomeDao();
            _logger = logger;
        }

        public HomeController()
        {
            homedao = new HomeDao();
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Register()
        {
            return View("_Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IncluiRegister(Cliente cliente, Pet pet) 
        {
            if(!ModelState.IsValid)
            {
                return View("_Register");
            }
            else
            {
                var result = homedao.IncluiRegister(cliente, pet);

                if (result == true)
                {
                    ViewBag.ResultRegister = "Cadastro Incluido, Faça seu Login Agora";
                    return View("_Login");
                }
                else
                {
                    ViewBag.ResultRegister = "Erro ao realizar cadastro";
                    return View("_Register");
                }
            }
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
