
using AppTurismoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppTurismoAPI.DbContexts
{
    public class AppTurismoContext : DbContext
    {
        public DbSet<Cidade> Cidades { get; set; } = null!;
        public DbSet<PontoTuristico> PontoTuristicos { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-NP92RCIQ\\SQLEXPRESS;Database=AppTurismo.Api;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
