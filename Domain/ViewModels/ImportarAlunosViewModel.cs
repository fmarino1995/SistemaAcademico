using Domain.Entities;

namespace Domain.ViewModels
{
    public class ImportarAlunosViewModel
    {
        public LogImportacao LogImportacao { get; set; }
        public int QuantidadeImportados { get; set; }
        public int QuantidadeNaoImportados { get; set; }
        public string StringLog { get; set; }
    }
}
