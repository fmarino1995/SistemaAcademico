using Domain.Entities;

namespace Domain.ViewModels
{
    public class ImportacaoAlunosDisciplinaViewModel
    {
        public LogImportacao LogImportacao { get; set; }
        public List<string> EmailAlunos { get; set; }
        public int QtdImportados { get; set; }
        public int QtdNaoImportados { get; set; }
    }
}
