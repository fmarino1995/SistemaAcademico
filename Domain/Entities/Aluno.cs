using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Aluno
    {
        [Key]
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        [MaxLength(11, ErrorMessage = "Formato inválido para CPF")]
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public string UsuarioCriacao { get; set; }
        public string Email { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string Status { get; set; }
        public string Turno { get; set; }
    }
}
