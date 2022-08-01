using Domain.Entities;

namespace Domain.ViewModels
{
    public class DisciplinaAlunoProfessorViewModel
    {
        public List<DisciplinaAluno> DisciplinasAlunosList { get; set; }
        public Professor Professor { get; set; }

    }
}
