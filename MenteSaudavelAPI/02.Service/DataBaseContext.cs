using MenteSaudavelAPI._03.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenteSaudavelAPI._02.Services
{
    public class DataBaseContext : DbContext
    {
        #region CONSTRUTOR
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        #endregion

        #region TABELAS
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Questionario> Questionario { get; set; }
        public DbSet<Resposta> Resposta { get; set; }
        #endregion

        #region METODOS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamentos
            modelBuilder.Entity<Questionario>()
                .HasOne(q => q.Respondente)
                .WithMany(u => u.Questionarios)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Questionario>()
                .HasMany(q => q.Respostas)
                .WithOne(r => r.Questionario)
                .OnDelete(DeleteBehavior.Cascade);

            // Colunas
            modelBuilder.Entity<Usuario>().Property(p => p.Nome).HasMaxLength(100);
            modelBuilder.Entity<Usuario>().Property(p => p.Senha).HasMaxLength(100);
            modelBuilder.Entity<Usuario>().ComplexProperty(cp => cp.Email).Property(p => p.Endereco).HasColumnName("Email").HasMaxLength(100);
            modelBuilder.Entity<Usuario>().ComplexProperty(cp => cp.Genero).Property(p => p.Valor).HasColumnName("Genero");

            modelBuilder.Entity<Questionario>().OwnsOne(o => o.Estratificacao, b =>
            {
                b.Property(p => p.Valor).HasColumnName("Estratificacao");
                b.Ignore(e => e.Descricao);
            });
            modelBuilder.Entity<Questionario>().Navigation(p => p.Estratificacao).IsRequired(false);
        }
        #endregion
    }
}