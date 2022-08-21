using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Aluno
    {
        [Key]
        public int AlunoId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Formato inválido para CPF")]
        public string Cpf { get; set; }
        [Required]
        public string Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        [Required]
        public string UsuarioCriacao { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Turno { get; set; }
    }
}
