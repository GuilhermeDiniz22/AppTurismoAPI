
using AppTurismoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppTurismoAPI.DbContexts
{
    public class AppTurismoContext : DbContext
    {
        public DbSet<Cidade> Cidades { get; set; } = null!;
        public DbSet<PontoTuristico> PontoTuristicos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-NP92RCIQ\\SQLEXPRESS;Database=AppTurismo.Api;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cidade>().HasData(
                new Cidade("São Paulo")
                {
                    Id = 1,
                    Descricao = "O Coração financeiro do Brasil."
                },
                new Cidade("Rio de Janeiro")
                {
                    Id = 2,
                    Descricao = "O cartão postal do Brasil."
                },
                new Cidade("Recife")
                {
                    Id = 3,
                    Descricao = "A cidade do carnaval."
                });
            modelBuilder.Entity<PontoTuristico>().HasData(
                new PontoTuristico("Feira livre")
                {
                    Id = 1,
                    CidadeId = 1,
                    Descricao = "A maior feira livre do mundo."
                },
                new PontoTuristico("Cristo redentor")
                {
                    Id = 2,
                    CidadeId = 2,
                    Descricao = "A estátua mais famosa do mundo."
                },
                new PontoTuristico("Praia de boa viagem")
                {
                    Id = 3,
                    CidadeId = 3,
                    Descricao = "A cidade do carnaval."
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
