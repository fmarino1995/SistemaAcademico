using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DisciplinaAluno
    {
        [Key]
        public int DisciplinasAlunosId { get; set; }
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
        public int DisciplinaId { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        public int TotalAulasValidas { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }
    }
}
