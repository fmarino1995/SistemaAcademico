using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PresencaAluno
    {
        [Key]
        public int PresencaAlunoId { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }
        public int DisciplinaId { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
        public DateTime DataPresenca { get; set; }
        public bool Presenca { get; set; }
        public bool Falta { get; set; }
    }
}
