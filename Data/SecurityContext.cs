using Microsoft.EntityFrameworkCore;
using SecurityState.API.Entities;

namespace SecurityState.API.Data
{
    public class SecurityContext : DbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        {
        }

        public DbSet<Incidente> Incidentes { get; set; }
        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações opcionais (tamanhos, required, relations)
            modelBuilder.Entity<Agente>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(a => a.Nome).HasMaxLength(200).IsRequired();
                b.Property(a => a.Funcao).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<Incidente>(b =>
            {
                b.HasKey(i => i.Id);
                b.Property(i => i.Tipo).HasMaxLength(150).IsRequired();
                b.Property(i => i.Descricao).HasMaxLength(2000);
                b.HasOne(i => i.Agente)
                 .WithMany(a => a.Incidentes)
                 .HasForeignKey(i => i.AgenteId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Relatorio>(b =>
            {
                b.HasKey(r => r.Id);
                b.Property(r => r.Conteudo).HasMaxLength(4000).IsRequired();
                b.HasOne(r => r.Incidente)
                 .WithMany()
                 .HasForeignKey(r => r.IncidenteId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
