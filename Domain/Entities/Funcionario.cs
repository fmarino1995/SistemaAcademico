using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Status { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        [Required]
        public string Cpf { get; set; }
    }
}
