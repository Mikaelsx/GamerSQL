using Microsoft.EntityFrameworkCore;
using ProjetoGameSQL.Models;

namespace ProjetoGameSQL.Infra
{
    public class Context : DbContext
    {
        public Context()
        {  
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = NOTE11-S13; initial catalog = gamerSQL; Integrated Security = true; TrustServerCertificate = true");
            }
        }

        public DbSet<Jogador> Jogador {get; set;}

        public DbSet<Equipe> Equipe {get; set;}
    }
}