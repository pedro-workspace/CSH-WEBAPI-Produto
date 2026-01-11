using ProdutosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ProdutoApi.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produto> Produtos {get;set;}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}