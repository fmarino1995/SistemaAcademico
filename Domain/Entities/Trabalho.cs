using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Trabalho
    {
        [Key]
        public int TrabalhoId { get; set; }
        public int DisciplinaId { get; set; }
        public virtual Disciplina Disciplina { get; set; }
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
        public int SemestreVigenteId { get; set; }
        public virtual SemestreVigente SemestreVigente { get; set; }
        public DateTime DataEnvio { get; set; }
        public double? Nota { get; set; }
        [Required]
        public string NomeArquivo { get; set; }
        [Required]
        public string CaminhoArquivo { get; set; }
        [Required]
        public string UsuarioAlteracao { get; set; }
        [Required]
        public string Hash { get; set; }
    }
}
