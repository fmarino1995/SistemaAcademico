using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class NotaAluno
    {
        [Key]
        public int NotaAlunoId { get; set; }
        public int DisciplinaAlunoId { get; set; }
        public virtual DisciplinaAluno DisciplinaAluno { get; set; }
        [DefaultValue(0.0)]
        public double NotaAvaliacao1 { get; set; }
        [DefaultValue(0.0)]
        public double NotaAvaliacao2 { get; set; }
        public double? ProvaFinal { get; set; }
        [DefaultValue(0.0)]
        public double NotaFinal { get; set; }
        public int QuantidadeFalta { get; set; }
        public int QuantidadePresenca { get; set; }
    }
}
