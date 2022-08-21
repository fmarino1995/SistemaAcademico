using Domain.Entities;

namespace Domain.ViewModels
{
    public class PresencaAlunosDisciplinaViewModel
    {
        public List<DisciplinaAluno> DisciplinasAlunoList { get; set; }
        public List<PresencaAluno> PresencaAlunos { get; set; }
    }
}
