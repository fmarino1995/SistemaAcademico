using Domain.Entities;

namespace Domain.ViewModels
{
    public class ImportarAlunosViewModel
    {
        public LogImportacao LogImportacao { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
