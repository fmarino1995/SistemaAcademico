using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DefaultValue(0.0)]
        public double NotaAvaliacao1 { get; set; }
        [DefaultValue(0.0)]
        public double NotaAvaliacao2 { get; set; }
        public double? ProvaFinal { get; set; }
        [DefaultValue(0.0)]
        public double NotaFinal { get; set; }
        public int QuantidadeFalta { get; set; }
        public int QuantidadePresenca { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataUltimaPresenca { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
