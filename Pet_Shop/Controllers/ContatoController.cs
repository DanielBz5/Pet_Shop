using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Contato()
        {
            return View();
        }
    }
}
