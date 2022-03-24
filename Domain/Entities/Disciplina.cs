using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class Disciplina
    {
        [Key]
        public int DisplicinaId { get; set; }
        [Required]
        public string Nome { get; set; }
        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
    }
}
