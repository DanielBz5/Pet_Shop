using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Dao
{
    public class ShopDao
    {
        private readonly ApplicationDbContext _context;
        public ShopDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Produto> BuscaProdutos(Func<Produto, bool> filtro)
        {
            try
            {
                List<Produto> Produtos = _context.Produto.Where(filtro).OrderByDescending(p => p.Cod).Take(10).ToList();
                return Produtos;
            }
            catch
            {
                return null;
            }
        }

        public List<Produto> BuscaProdutosFiltro(Func<Produto, bool> filtro)
        {
            try
            {
                return _context.Produto.Where(filtro).ToList();
            }
            catch
            {
                return null;
            }
            
        }

        public bool IncluiProduto(Produto produto)
        {
            try
            {
                _context.Produto.Add(produto);
                int linhasAfetadas = _context.SaveChanges();
                return linhasAfetadas > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public Produto ConsultaProduto(Produto produto)
        {
            try
            {
                return produto = _context.Produto.SingleOrDefault(p => p.Cod == produto.Cod);
            }
            catch
            {
                return null;
            }
        }

        public bool AtualizaProduto(Produto produtoOld, Produto produto)
        {
            try
            {
                produtoOld.Valor = produto.Valor;
                produtoOld.Quantidade = produto.Quantidade;
                produtoOld.Estoque_Minimo = produto.Estoque_Minimo;

                int linhasAfetadas = _context.SaveChanges();
                return linhasAfetadas > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public bool RegistraEstoque(Estoque estoque)
        {
            try
            {
                _context.Estoque.Add(estoque);
                int linhasAfetadas = _context.SaveChanges();
                return linhasAfetadas > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public List<Produto> ControleMinimo()
        {
            try
            {
                return _context.Produto.Where(p => p.Quantidade < p.Estoque_Minimo).ToList();
            }
            catch
            {
                return null;
            }

        }
    }
}
