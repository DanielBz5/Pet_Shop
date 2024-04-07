using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pet_Shop.Controllers
{
    public class SegurancaController : Controller
    {
        public HomeDao homedao;

        [ActivatorUtilitiesConstructor]
        public SegurancaController()
        {
            homedao = new HomeDao();
        }

        public IActionResult Login(string returnUrl)//CookieAuthenticationOptions.ReturnUrlParameter
        {
            ViewBag.returnUrl = returnUrl ?? "/";
            return View("_Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Cliente cliente, string returnUrl)
        {
            if (homedao.ValidaLogin(cliente) != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, cliente.Nome),
                    new Claim(ClaimTypes.NameIdentifier, cliente.Senha)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = cliente.RememberMe
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return Redirect(returnUrl ?? "/");

            }

            ViewBag.AcessoNegado = "Acesso Negado, Esse Usuario não Existe";
            return View("_Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
