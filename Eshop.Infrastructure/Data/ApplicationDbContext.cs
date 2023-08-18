using Eshop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Eshop.Infrastructure.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        private void CreateModelProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(b => b.Id)
                .HasName("PK_Product_Id");
            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .Property(x => x.Id).IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.ImgUri).IsRequired();

            modelBuilder.Entity<Product>()
                .Property(x => x.Price).HasPrecision(18, 4).IsRequired();
        }

        private void DataSeedProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Rohlík", ImgUri = "https://rohlik.img", Price = 3.5M },
                new Product() { Id = 2, Name = "Houska", ImgUri = "https://houska.img", Price = 3.5M },
                new Product() { Id = 3, Name = "Knedlík", ImgUri = "https://knedlik.img", Price = 24.9M, Description = "houskový knedlík" },
                new Product() { Id = 4, Name = "Vánočka", ImgUri = "https://vanocka.img", Price = 49.9M });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CreateModelProduct(modelBuilder);
            DataSeedProduct(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
