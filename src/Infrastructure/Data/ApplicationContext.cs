using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Product> Products { get; set; }


        private readonly bool isTestingEnviroment;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTestingEnviroment = false) : base(options)
        {
            this.isTestingEnviroment = isTestingEnviroment;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de unicidad para UserName y Email en User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configuración de herencia
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("User")
                .HasValue<Admin>("Admin")
                .HasValue<Client>("Client");

            // Configuración de Client a Sale (uno a muchos)
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Client)
                .HasForeignKey(s => s.ClientId);

            // Configuración de Sale a Line (uno a muchos)
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Lines)
                .WithOne(l => l.Sale)
                .HasForeignKey(l => l.SaleId);

            // Configuración de Line a Product (muchos a uno)
            modelBuilder.Entity<Line>()
                .HasOne(l => l.Product)
                .WithMany() // No se establece la relación inversa en Product
                .HasForeignKey(l => l.ProductId);
        }
    }
}
