using Microsoft.EntityFrameworkCore;
using Pet_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Dao
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pet> Pets { get; set; }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Estoque> Estoque { get; set; }
    }
}
