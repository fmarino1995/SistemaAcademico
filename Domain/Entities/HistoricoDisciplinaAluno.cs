using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class HistoricoDisciplinaAluno
    {
        [Key]
        public int HistoricoDisciplinaAlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public bool Finalizado { get; set; }
    }
}
