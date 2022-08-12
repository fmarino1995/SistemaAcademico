using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Aviso
    {
        [Key]
        public int AvisoId { get; set; }
        [Required(ErrorMessage = "Digite um título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Digite um texto")]
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        [Required]
        public string UsuarioCriacao { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
