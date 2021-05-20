using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevIO.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Previne que nos casos de falta de configuração no Fluent API, os campos de texto não sejam criados como nvarchar(MAX)
            modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties())
                .Where(p => p.ClrType == typeof(string)).ToList()
                .ForEach(p => p.Relational().ColumnType = "varchar(100)");

            //Faz o Mapping automaticamente com as classes que implementam IEntityTypeConfiguration
            //Evita a necessidade de mapear manualmente para cada classe
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            //Previne o DeleteCascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
