using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class Aluno
    {
        [Key]
        public int AlunoId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
