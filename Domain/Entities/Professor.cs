using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Professor
    {
        [Key]
        public int ProfessorId { get; set; }
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
