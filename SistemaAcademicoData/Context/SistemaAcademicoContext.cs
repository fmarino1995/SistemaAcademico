using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoData.Context
{
    public class SistemaAcademicoContext : IdentityDbContext<ApplicationUser>
    {
        public SistemaAcademicoContext(DbContextOptions<SistemaAcademicoContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<LogImportacao> LogImportacoes { get; set; }
        public DbSet<DisciplinaAluno> DisciplinasAlunos { get; set; }
        public DbSet<Aviso> Avisos { get; set; }
        public DbSet<HistoricoDisciplinaAluno> HistoricoDisciplinaAlunos { get; set; }
        public DbSet<SemestreVigente> SemestresVigentes { get; set; }
        public DbSet<PresencaAluno> PresencaAlunos { get; set; }
        public DbSet<Trabalho> Trabalhos { get; set; }
        public DbSet<RespostaAviso> RespostaAvisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaAcademicoContext).Assembly);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Aluno>(a =>
            {
                a.HasIndex(a => a.AlunoId).IsUnique();
                a.Property(a => a.Nome).IsRequired();
                a.Property(a => a.Cpf).IsRequired();
                a.Property(a => a.Matricula).IsRequired();
                a.Property(a => a.UsuarioCriacao).IsRequired();
                a.Property(a => a.ApplicationUserId).IsRequired();
                a.Property(a => a.Status).IsRequired();
                a.Property(a => a.Turno).IsRequired();
                a.Property(a => a.Email).IsRequired();
            });
            

            

        }
    }
}