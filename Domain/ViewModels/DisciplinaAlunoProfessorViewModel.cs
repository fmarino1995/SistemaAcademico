using Domain.Entities;

namespace Domain.ViewModels
{
    public class DisciplinaAlunoProfessorViewModel
    {
        public List<DisciplinaAluno> DisciplinasAlunoList { get; set; }
        public string Turno { get; set; }
        public int DisciplinaId { get; set; }
    }
}
