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
        [Required(ErrorMessage = "Perfil é obrigatório")]
        public string RoleId { get; set; }

        [NotMapped]
        public string Senha { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        [NotMapped]
        public string StatusFull { get; set; }
    }
}
