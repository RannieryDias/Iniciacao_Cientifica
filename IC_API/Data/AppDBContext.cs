using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IC_API.Models;
using IC_API.Model;

namespace IC_API.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deputado>()
                .HasOne(p => p.ultimoStatus);

            modelBuilder.Entity<Tramitacao>()
                .HasOne(p => p.projeto);
        }

        public DbSet<IC_API.Models.Projeto> Projeto { get; set; }
        public DbSet<IC_API.Model.Deputado> Deputado { get; set; }
        public DbSet<IC_API.Models.Partido> Partido { get; set; }
        public DbSet<IC_API.Models.ProjetoDetalhado> ProjetoDetalhado { get; set; }
        public DbSet<IC_API.Models.StatusProposicao> StatusProposicao { get; set; }
        public DbSet<IC_API.Models.Autor> Autor { get; set; }
        public DbSet<IC_API.Models.Tramitacao> Tramitacao { get; set; }
    }
}
