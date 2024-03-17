using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Pet_Shop.Dao;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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


        //public IActionResult CreateReport(List<Estoque> Estoque)
        //{
        //    var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\Estoque.frx");
        //    var reportFile = caminhoReport;
        //    var freport = new FastReport.Report();


        //    freport.Dictionary.RegisterBusinessObject(Estoque, "estoqueList", 10, true);
        //    freport.Report.Save(reportFile);

        //    return Ok($" Relatorio gerado : {caminhoReport}");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Relatorios(Relatorio relatorio)// não está passando os filtros //arruma isso
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
                    
                    return ReportView(Produtos, null);
                }
                else if (relatorio.Modelo == "Estoque")
                {
                    List<Estoque> Estoque = new List<Estoque>();
                    Func<Estoque, bool> filtro = e => true;

                    if (relatorio.DataInicial != DateTime.MinValue && relatorio.DataFinal != DateTime.MinValue)
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
                    return ReportView(null ,Estoque);
                }

                return View("Relatorios");
            }
            else
            {
                return View("Relatorios");
            }
        }

        public IActionResult ReportView(List<Produto> produtos, List<Estoque> estoque)
        {
            List<dynamic> RelModel = new List<dynamic>();
            var RelName = "";
            var dbSet = "";
            if (produtos != null)
            {
                RelModel.AddRange(produtos);
                RelName = "Produtos";
                dbSet = "productList";
            }
            else if(estoque != null)
            {
                RelModel.AddRange(estoque);
                RelName = "Estoque";
                dbSet = "estoqueList";
            }

            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"reports\"+RelName+".frx");
            var reportFile = caminhoReport;
            var freport = new FastReport.Report();
            

            freport.Report.Load(reportFile);
            freport.Dictionary.RegisterBusinessObject(RelModel, dbSet, 10, true);
            freport.Prepare();

            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }


        public IActionResult ImportProdutos()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportProdutos(IFormFile excelFile, IFormFileCollection Imagens )
        {
            if(await ImportaImagens(Imagens) != true)
                return View("MessageBox", TempData["Titulo"] = "Atenção!");

            if (ValidaExcel(excelFile) != true)
                return View("MessageBox", TempData["Titulo"] = "Atenção!");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(excelFile.OpenReadStream()))
            {
                var workbook = package.Workbook;
                if (workbook != null)
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet != null)
                    {
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;
                        var TotalProdutos = 0;
                        var ErroProdutos = 0;


                        for (int row = 2; row <= rowCount; row++) // Começa na linha 2
                        {
                            try
                            {
                                Produto produto = new Produto
                                {
                                    Imagem = BuscaBinario(worksheet.Cells[row, 1].Value?.ToString()),
                                    Nome = worksheet.Cells[row, 2].Value?.ToString(),
                                    Valor = Convert.ToDouble(worksheet.Cells[row, 3].Value),
                                    Estoque_Minimo = Convert.ToInt32(worksheet.Cells[row, 4].Value),
                                    Categoria = worksheet.Cells[row, 5].Value?.ToString(),
                                    Descricao = worksheet.Cells[row, 6].Value?.ToString(),
                                };

                                if (TryValidateModel(produto) && produto.Imagem != null)
                                {
                                    if (shopdao.IncluiProduto(produto))
                                        TotalProdutos++;
                                }
                                else
                                {
                                    ErroProdutos++;
                                }
                            }
                            catch
                            {
                                ErroProdutos++;
                            } 
                        }

                        return View("MessageBox", (TempData["Mensagem"] = "Foi Importado "+TotalProdutos+" Produtos, e "+ ErroProdutos + " apresentou erro.", TempData["Titulo"] = "Sucesso!"));
                    }
                }

                return View("MessageBox", (TempData["Mensagem"] = "Erro na importação", TempData["Titulo"] = "Atenção!"));
            }
        }


        private async Task<bool> ImportaImagens(IFormFileCollection imagens)
        {
            try
            {
                LimpaTempImg();

                if (imagens == null || imagens.Count == 0)
                {
                    TempData["Mensagem"] = "Nenhuma Imagem Selecionada";
                    return false;
                }

                var Diretorio = Path.Combine(_webHostEnv.WebRootPath, "img", "temp");

                if (!Directory.Exists(Diretorio))
                    Directory.CreateDirectory(Diretorio); // Cria diretorio se não existir

                foreach (var imagem in imagens)
                {
                    var extensao = Path.GetExtension(imagem.FileName).ToLower();
                    if (extensao == ".png" || extensao == ".jpeg" || extensao == ".jpg")
                    {
                        var filePath = Path.Combine(Diretorio, imagem.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imagem.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = "Um ou mais arquivos não são imagens válidas. Só é permitido Imagens .png .jpeg .jpg";
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = "Erro na Importação de Imagens:" + ex.Message;
                return false;
            }
        }


        public void LimpaTempImg()
        {
            var Diretorio = Path.Combine(_webHostEnv.WebRootPath, "img", "temp");
            DirectoryInfo tempDir = new DirectoryInfo(Diretorio);
            foreach (FileInfo file in tempDir.GetFiles())
            {
                file.Delete();
            }
        }

        private bool ValidaExcel(IFormFile excelFile)
        {
            if (excelFile == null)
            {
                TempData["Mensagem"] = "Nenhuma Planilha Excel Seleciona";
                return false;
            }
            else if (!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Mensagem"] = "Arquivo no formato invalido, Use o formato .xlsx";
                return false;
            }

            return true;
        }

        private byte[] BuscaBinario(string NameImg)
        {
            try
            {
                byte[] imgBinario = null;

                var diretorio = Path.Combine(_webHostEnv.WebRootPath, "img", "temp");
                var tempDir = new DirectoryInfo(diretorio);

                var imagem = tempDir.GetFiles("*.*", SearchOption.TopDirectoryOnly)
                                      .Where(file => new[] { ".png", ".jpg", ".jpeg" }.Any(ext => file.Name.Equals(NameImg + ext, StringComparison.OrdinalIgnoreCase)))
                                      .ToArray();//Busca os arquivos no diretorio e compara se algum coresponde ao nome+opções de extenção e retorna martrix

                if (imagem.Length > 0)
                {
                    var arquivo = imagem[0];
                    using (var ms = new MemoryStream())
                    using (var fileStream = arquivo.OpenRead()) //le
                    {
                        fileStream.CopyTo(ms); //copia content
                        imgBinario = ms.ToArray(); //gera byte
                    }
                }

                return imgBinario;
            }
            catch
            {
                return null;
            }    
        }

        public IActionResult ExportProdutos()
        {
            try
            {
                LimpaTemp();

                List<Produto> Produtos = new List<Produto>();
                Func<Produto, bool> filtro = p => true;
                Produtos = shopdao.BuscaProdutos(filtro);

                if (Produtos != null)
                {
                    var filePath = Path.Combine(_webHostEnv.WebRootPath, "Temp", "Produtos.xlsx");

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var excel = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = excel.Workbook.Worksheets.Add("Produtos");

                        //defini propriedades
                        worksheet.TabColor = System.Drawing.Color.Black; //cor row
                        worksheet.DefaultRowHeight = 12; //tamanho row

                        //propriedadea primeira linha
                        worksheet.Row(1).Height = 20;
                        worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Row(1).Style.Font.Bold = true;

                        //define cabeçalho
                        worksheet.Cells[1, 1].Value = "Imagem";
                        worksheet.Cells[1, 2].Value = "Codigo";
                        worksheet.Cells[1, 3].Value = "Nome";
                        worksheet.Cells[1, 4].Value = "Descrição";
                        worksheet.Cells[1, 5].Value = "Valor";
                        worksheet.Cells[1, 6].Value = "Quantidade";
                        worksheet.Cells[1, 7].Value = "Estoque Minimo";
                        worksheet.Cells[1, 8].Value = "Categoria";


                        int row = 2;
                        foreach (var produto in Produtos)
                        {
                            worksheet.Cells[row, 1].Value = produto.Imagem;
                            worksheet.Cells[row, 2].Value = produto.Cod;
                            worksheet.Cells[row, 3].Value = produto.Nome;
                            worksheet.Cells[row, 4].Value = produto.Descricao;
                            worksheet.Cells[row, 5].Value = produto.Valor;
                            worksheet.Cells[row, 6].Value = produto.Quantidade;
                            worksheet.Cells[row, 7].Value = produto.Estoque_Minimo;
                            worksheet.Cells[row, 8].Value = produto.Categoria;
                            row++;
                        }

                        // Ajusta tamanho colunas
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        excel.Save();
                    }

                    // Retorna o arquivo Excel como um FileStreamResult para abrir o explorador de arquivos
                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Produtos.xlsx");
                }
                else
                {
                    return View("MessageBox", (TempData["Mensagem"] = "Nenhum produto encontrado", TempData["Titulo"] = "Atenção!"));
                }
            }
            catch (Exception ex)
            {
                return View("MessageBox", (TempData["Mensagem"] = $"Não foi possível exportar o Excel. Erro: {ex.Message}", TempData["Titulo"] = "Atenção!"));
            }
        }

        public void LimpaTemp()
        {
            string tempFolderPath = Path.Combine(_webHostEnv.WebRootPath, "Temp");
            DirectoryInfo tempDir = new DirectoryInfo(tempFolderPath);
            foreach (FileInfo file in tempDir.GetFiles())
            {
                file.Delete();
            }
        }

    }


}
