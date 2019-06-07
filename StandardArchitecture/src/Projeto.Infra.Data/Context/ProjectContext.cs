using Architecture.Infra.CrossCutting.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Domain.Clientes;
using Project.Domain.Compras;
using Project.Infra.Data.Mappings;
using System.IO;

namespace Project.Infra.Data.Context
{
    public class ProjectContext : DbContext
    {
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new CompraMapping());
            modelBuilder.AddConfiguration(new ClienteMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
