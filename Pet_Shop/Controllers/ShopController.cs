using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pet_Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopDao shopdao;
        public ShopController(ApplicationDbContext context)
        {
            shopdao = new ShopDao(context);
        }

        public IActionResult Shop()
        {
            Cliente cliente = new Cliente();
            cliente.Nome = User.Identity.Name;
            cliente.Senha = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (cliente.Nome == "Admin")
            {
                return RedirectToAction("ManageShop");
            }

            Func<Produto, bool> filtro = p => p.Quantidade > 0;
            List<Produto> Produtos = shopdao.BuscaProdutos(filtro);
            if(Produtos.Count == 0 && Produtos == null)
            {
                return View("MessageBox", (TempData["Mensagem"] = "Não foi encontrado produtos com estoque", TempData["Titulo"] = "Atenção!"));
            }
            else
            {
                return View(Produtos);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FiltroShop (Produto produto)
        {
            var Produtos = Filtro(produto);
            return View("Shop", Produtos);
        }

        public List<Produto> Filtro (Produto produto)
        {
            List<Produto> Produtos = new List<Produto>();

            if (produto.Categoria != null)
            {
                if (produto.Nome != null)
                {
                    //categoria e nome
                    Func<Produto, bool> filtro = p => p.Nome.Contains(produto.Nome) && p.Categoria == produto.Categoria;
                    Produtos = shopdao.BuscaProdutosFiltro(filtro);
                }
                else
                {
                    //só categoria
                    Func<Produto, bool> filtro = p => p.Categoria == produto.Categoria;
                    Produtos = shopdao.BuscaProdutosFiltro(filtro);
                }
            }
            else
            {
                //só nome
                Func<Produto, bool> filtro = p => p.Nome.Contains(produto.Nome);
                Produtos = shopdao.BuscaProdutosFiltro(filtro);
            }

            return Produtos;
        }

        public IActionResult ManageShop()
        {
            return View();
        }

        public IActionResult AddProduto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduto(Produto produto)
        {
            var imagem = HttpContext.Request.Form.Files.FirstOrDefault(); //acessa arquivos post
            if(imagem == null)
            {
                ModelState.AddModelError("Imagem", "Escolha uma Imagem para o Produto");
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    imagem.CopyTo(ms); // Le imagem p MemoryStream
                    produto.Imagem = ms.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                if (shopdao.IncluiProduto(produto))
                {
                    return View("MessageBox", TempData["Mensagem"] = "Produto Cadastrado com Sucesso!");
                }
                else
                {
                    return View("MessageBox", (TempData["Mensagem"] = "Erro ao Cadatrar Produto", TempData["Titulo"] = "Atenção!"));
                }
            }
            else
            {
                return View("AddProduto");
            }
        }

        public IActionResult ConsultaProduto()
        {
            Func<Produto, bool> filtro = p => p.Quantidade >= 0;
            List<Produto> Produtos = shopdao.BuscaProdutos(filtro);
            return View(Produtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConsultaProduto(Produto produto)
        {
            var Produtos = Filtro(produto);
            return View(Produtos);
        }

        [HttpGet]
        public IActionResult GerenciaProduto(Produto produto)
        {
            produto = shopdao.ConsultaProduto(produto);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GerenciaProdutoPost(Produto produto)
        {
            if (ModelState.IsValid)
            {
                Produto produtoOld = shopdao.ConsultaProduto(produto);
                if (shopdao.AtualizaProduto(produtoOld, produto))
                {
                    return View("MessageBox", TempData["Mensagem"] = "Alteração realizada com Sucesso!");
                }
         

                    //Produto produtoOld = shopdao.ConsultaProduto(produto);
                    //if (produto.Quantidade != produtoOld.Quantidade)
                    //{
                    //    Estoque estoque = new Estoque
                    //    {
                    //        Cod_Produto = produtoOld.Cod,
                    //        Nome = produtoOld.Nome,
                    //        Tipo_Movimento = produto.Quantidade > produtoOld.Quantidade ? "Entrada" : "Saida",
                    //        Data_ = DateTime.Now,
                    //        Quantidade = produto.Quantidade > produtoOld.Quantidade ? produto.Quantidade - produtoOld.Quantidade : produtoOld.Quantidade - produto.Quantidade,
                    //    };

                    //    if (shopdao.AtualizaProduto(produtoOld, produto))
                    //    {
                    //        if (!shopdao.RegistraEstoque(estoque))
                    //        {
                    //            shopdao.AtualizaProduto(produto, produtoOld);
                    //            return View("MessageBox", (TempData["Mensagem"] = "Não foi possivel Alterar Produto", TempData["Titulo"] = "Atenção!"));
                    //        }

                    //        return View("MessageBox", TempData["Mensagem"] = "Atualização realizada com Sucesso!");
                    //    }
                    //}  
            }
            else
            {
                return View("GerenciaProduto");
            }

            
            return View("MessageBox", (TempData["Mensagem"] = "Não foi possivel Alterar Produto", TempData["Titulo"] = "Atenção!"));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletaProduto(Produto produto)
        {
            produto = shopdao.ConsultaProduto(produto);
            if (shopdao.DeletaProduto(produto))
            {
                return RedirectToAction("ConsultaProduto");
            }
            else
            {
                return View("MessageBox", (TempData["Mensagem"] = "Não foi possivel Excluir Produto", TempData["Titulo"] = "Atenção!"));
            }
        }

        public IActionResult ControleEstoque()
        {
            List<Produto> Produtos = shopdao.ControleMinimo();
            return View(Produtos);
        }

        public IActionResult ConsultaEstoque()
        {
            Func<Produto, bool> filtro = p => p.Quantidade >= 0;
            List<Produto> Produtos = shopdao.BuscaProdutos(filtro);
            return View(Produtos);
        }

        public IActionResult MovimentoEstoque(Produto produto)
        {
            return View("MovimentoEstoque", produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MovimentoEstoquePost(Produto produto, Estoque estoque)
        {
            estoque.Cod_Produto = produto.Cod;
            estoque.Nome = produto.Nome;
            estoque.Data_ = DateTime.Now;

            if (ModelState.IsValid)
            {
                produto.Quantidade = estoque.Tipo_Movimento == "Entrada" ? produto.Quantidade + estoque.Quantidade : produto.Quantidade - estoque.Quantidade;

                if(produto.Quantidade >= 0)
                {
                    // falta atualiza produto
                    shopdao.RegistraEstoque(estoque);
                }
                else
                {
                    return View("MessageBox", (TempData["Mensagem"] = "O Estoque não pode ficar negativo", TempData["Titulo"] = "Atenção!"));
                }
            }
            else
            {
                return View("MovimentoEstoque");
            }
        }


    }


}
