using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas => Set<Pessoa>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Transacao> Transacoes => Set<Transacao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Pessoa
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nome)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Idade)
                      .IsRequired();
            });

            // Configuração da entidade Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Descricao)
                      .IsRequired()
                      .HasMaxLength(400);

                entity.Property(c => c.Finalidade)
                      .IsRequired();
            });

            // Configuração da entidade Transacao
            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Descricao)
                      .IsRequired()
                      .HasMaxLength(400);

                entity.Property(t => t.Valor)
                      .IsRequired()
                      .HasColumnType("numeric(18,2)");

                entity.Property(t => t.Tipo)
                      .IsRequired();

                // Relacionamento: uma Pessoa possui muitas Transacoes
                entity.HasOne(t => t.Pessoa)
                      .WithMany(p => p.Transacoes)
                      .HasForeignKey(t => t.PessoaId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relacionamento: uma Categoria possui muitas Transacoes
                entity.HasOne(t => t.Categoria)
                      .WithMany(c => c.Transacoes)
                      .HasForeignKey(t => t.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}