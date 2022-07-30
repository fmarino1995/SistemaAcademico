using Domain.Entities;

namespace Domain.ViewModels
{
    public class ImportarUsuariosViewModel
    {
        public LogImportacao LogImportacao { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
