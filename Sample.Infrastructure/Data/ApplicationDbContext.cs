using Sample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sample.Infrastructure.Data
{
    /// <summary>
    /// Represents application database structure, set all relations and can be used to query and save instances of entities.
    /// </summary>
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        private void CreateModelProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(b => b.Id).HasName("PK_Product_Id");
            
            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 4);
        }

        private void DataSeedProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Rohlík", ImgUri = new Uri("https://rohlik.img"), Price = 3.5M },
                new Product() { Id = 2, Name = "Houska", ImgUri = new Uri("https://houska.img"), Price = 3.5M },
                new Product() { Id = 3, Name = "Knedlík", ImgUri = new Uri("https://knedlik.img"), Price = 24.9M, Description = "houskový knedlík" },
                new Product() { Id = 4, Name = "Vánočka", ImgUri = new Uri("https://vanocka.img"), Price = 49.9M },
                new Product() { Id = 5, Name = "Pomeranč", ImgUri = new Uri("https://pomeranc.img"), Price = 5M },
                new Product() { Id = 6, Name = "Kiwi", ImgUri = new Uri("https://kiwi.img"), Price = 7M },
                new Product() { Id = 7, Name = "Sýr", ImgUri = new Uri("https://syr.img"), Price = 18.9M },
                new Product() { Id = 8, Name = "Jogurt", ImgUri = new Uri("https://jogurt.img"), Price = 25.9M },
                new Product() { Id = 9, Name = "Salám", ImgUri = new Uri("https://salam.img"), Price = 69.9M },
                new Product() { Id = 10, Name = "Mléko", ImgUri = new Uri("https://mleko.img"), Price = 14.9M },
                new Product() { Id = 11, Name = "Paprika", ImgUri = new Uri("https://paprika.img"), Price = 20M },
                new Product() { Id = 12, Name = "Patizon", ImgUri = new Uri("https://patizon.img"), Price = 22M });
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
