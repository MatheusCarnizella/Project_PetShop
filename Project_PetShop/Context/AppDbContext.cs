using Microsoft.EntityFrameworkCore;
using Project_PetShop.Models;

namespace Project_PetShop.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        { }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Produto>? Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Usuario>().HasKey(u => u.Id);
            model.Entity<Usuario>().Property(u => u.userName).IsRequired();
            model.Entity<Usuario>().Property(u => u.Password).IsRequired();
            model.Entity<Usuario>().Property(u => u.Email).IsRequired();

            model.Entity<Produto>().HasKey(p => p.idProduto);
            model.Entity<Produto>().Property(p => p.nomeProduto).IsRequired();
            model.Entity<Produto>().Property(p => p.produtoDescricao).IsRequired();
            model.Entity<Produto>().Property(p => p.Valor).IsRequired();
            model.Entity<Produto>().Property(p => p.Quantidade).IsRequired();
            model.Entity<Produto>().Property(p => p.urlImagem).IsRequired();

            model.Entity<Produto>()
                .HasOne<Usuario>(p => p.Usuario)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.Id);
        }
    }
}



