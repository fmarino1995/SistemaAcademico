namespace Dominio.Models
{
    public class Professor
    {
        public int ProfessorId { get; set; }
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
