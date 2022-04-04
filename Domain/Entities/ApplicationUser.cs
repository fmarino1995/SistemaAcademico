using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }
        public string Status { get; set; }
        [Display(Name = "Data de criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Data de alteração")]
        public DateTime DataAlteracao { get; set; }
        [Display(Name = "Último Login")]
        public DateTime UltimoLogin { get; set; }
        public Guid RoleId { get; set; }

        [NotMapped]
        public string PasswordHashComfirm { get; set; }
    }
}
