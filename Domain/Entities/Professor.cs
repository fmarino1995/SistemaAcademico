using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Professor
    {
        [Key]
        public int ProfessorId { get; set; }
        public string Nome { get; set; }
        [Required]
        public string Status { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Matricula { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        [Required]
        public string UsuarioCriacao { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
