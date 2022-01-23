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
                .HasKey(t => new { t.projetoId, t.sequencia });

            modelBuilder.Entity<Autor>()
                .HasKey(a => new { a.idProjeto, a.codDeputado });

            modelBuilder.Entity<ProjetoTema>()
                .HasKey(p => new { p.idProjeto, p.idTema });

            modelBuilder.Entity<Mesa>()
                .HasKey(m => new { m.id, m.idLegislatura, m.codTitulo });
        }

        public DbSet<IC_API.Models.Projeto> Projeto { get; set; }
        public DbSet<IC_API.Model.Deputado> Deputado { get; set; }
        public DbSet<IC_API.Models.Partido> Partido { get; set; }
        public DbSet<IC_API.Models.ProjetoDetalhado> ProjetoDetalhado { get; set; }
        public DbSet<IC_API.Models.StatusProposicao> StatusProposicao { get; set; }
        public DbSet<IC_API.Models.Autor> Autor { get; set; }
        public DbSet<IC_API.Models.Tramitacao> Tramitacao { get; set; }
        public DbSet<IC_API.Models.Tema> Tema { get; set; }
        public DbSet<IC_API.Models.ProjetoTema> ProjetoTema { get; set; }
        public DbSet<IC_API.Models.Legislatura> Legislatura { get; set; }
        public DbSet<IC_API.Models.Mesa> Mesa { get; set; }
    }
}
