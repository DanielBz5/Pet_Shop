using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public readonly IWebHostEnvironment _webHostEnv;
        public ShopController(ApplicationDbContext context, IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
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
                    return View("MessageBox", (TempData["Mensagem"] = "Produto Cadastrado.", TempData["Titulo"] = "Sucesso!"));
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
                    return View("MessageBox", (TempData["Mensagem"] = "Produto Alterado.", TempData["Titulo"] = "Sucesso!"));
                }
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
            produto = shopdao.ConsultaProduto(produto);
            var ProdutoEstoque = new ProdutoEstoqueViewModel
            {
                Cod = produto.Cod,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                QuantidadeAtual = produto.Quantidade
            };
            return View("MovimentoEstoque", ProdutoEstoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MovimentoEstoquePost(ProdutoEstoqueViewModel ProdutoEstoque)// arruma o delete 
        {
            ProdutoEstoque.Data_ = DateTime.Now;

            if (ModelState.IsValid)
            {
                var QuantidadeInicial = ProdutoEstoque.QuantidadeAtual;
                ProdutoEstoque.QuantidadeAtual = ProdutoEstoque.TipoMovimento == "Entrada" ?
                                                 ProdutoEstoque.QuantidadeAtual + ProdutoEstoque.QuantidadeMovimento :
                                                 ProdutoEstoque.QuantidadeAtual - ProdutoEstoque.QuantidadeMovimento;

                if(ProdutoEstoque.QuantidadeAtual >= 0)
                {
                    var produto = new Produto {Cod = ProdutoEstoque.Cod,};

                    Produto produtoOld = shopdao.ConsultaProduto(produto);

                    var produtoNew =  shopdao.ConsultaProduto(produto);
                    produtoNew.Quantidade = ProdutoEstoque.QuantidadeAtual;

                    if (shopdao.AtualizaProduto(produtoOld, produtoNew))
                    {
                        var estoque = new Estoque
                        { 
                            Cod_Produto = ProdutoEstoque.Cod, 
                            Nome = ProdutoEstoque.Nome,
                            Tipo_Movimento = ProdutoEstoque.TipoMovimento,
                            Data_ = ProdutoEstoque.Data_,
                            Quantidade = ProdutoEstoque.QuantidadeMovimento,
                            Descricao = ProdutoEstoque.Descricao
                        };

                        if (shopdao.RegistraEstoque(estoque))
                        {
                            return View("MessageBox", (TempData["Mensagem"] = "Movimentação do Estoque Concluído", TempData["Titulo"] = "Sucesso!"));
                        }
                        else
                        {
                            produtoOld.Quantidade = QuantidadeInicial;
                            shopdao.AtualizaProduto(produtoOld, produtoOld);
                            return View("MessageBox", (TempData["Mensagem"] = "Não foi possivel Movimentar Estoque", TempData["Titulo"] = "Atenção!"));
                        }
                    }
                    else
                    {
                        return View("MessageBox", (TempData["Mensagem"] = "Não foi possivel Alterar Produto", TempData["Titulo"] = "Atenção!"));
                    }
                }
                else
                {
                    return View("MessageBox", (TempData["Mensagem"] = "O Estoque não pode ficar negativo", TempData["Titulo"] = "Atenção!"));
                }
            }
            else
            {
                return View("MovimentoEstoque", ProdutoEstoque);
            }
        }

        public IActionResult Relatorios()
        {
            return View();
        }

        
        //public IActionResult CreateReport(List<Produto> Produtos)
        //{
        //    var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\Produtos.frx");
        //    var reportFile = caminhoReport;
        //    var freport = new FastReport.Report();
            

        //    freport.Dictionary.RegisterBusinessObject(Produtos, "productList", 10, true);
        //    freport.Report.Save(reportFile);

        //    return Ok($" Relatorio gerado : {caminhoReport}");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Relatorios(Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                if (relatorio.Modelo == "Produto")
                {
                    List<Produto> Produtos = new List<Produto>();
                    Func<Produto, bool> filtro = p => true;

                    if (relatorio.Categoria != null)
                    {
                        filtro = p => p.Categoria == relatorio.Categoria;
                    }

                    Produtos = shopdao.BuscaProdutosFiltro(filtro);
                    
                    return ReportView(Produtos);
                }
                else if (relatorio.Modelo == "Estoque")
                {
                    List<Estoque> Estoque = new List<Estoque>();
                    Func<Estoque, bool> filtro = e => true;

                    if (relatorio.DataInicial != null && relatorio.DataFinal != null)
                    {
                        filtro = e => filtro(e) && e.Data_ >= relatorio.DataInicial && e.Data_ <= relatorio.DataFinal;
                    }
                    else if (relatorio.Codigo != 0)
                    {
                        filtro = e => filtro(e) && e.Cod_Produto == relatorio.Codigo;
                    }
                    else if (relatorio.TipoMovimento != null)
                    {
                        filtro = e => filtro(e) && e.Tipo_Movimento == relatorio.TipoMovimento;
                    }

                    Estoque = shopdao.BuscaEstoque(filtro);
                    ViewBag.ReportData = Estoque;
                    return View("ReportView");
                }

                return View("Relatorios");
            }
            else
            {
                return View("Relatorios");
            }
        }

        public IActionResult ReportView(List<Produto> produtos)
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\Produtos.frx");
            var reportFile = caminhoReport;
            var freport = new FastReport.Report();
            

            freport.Report.Load(reportFile);
            freport.Dictionary.RegisterBusinessObject(produtos, "productList", 10, true);
            freport.Prepare();

            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }

    }


}
