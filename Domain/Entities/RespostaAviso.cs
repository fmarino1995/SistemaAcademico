using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RespostaAviso
    {
        [Key]
        public int RespostaAvisoId { get; set; }
        [Required]
        public string Mensagem { get; set; }
        public DateTime DataHoraResposta { get; set; }
        public int AvisoId { get; set; }
        public virtual Aviso Aviso { get; set; }
        public string UsuarioCriacao { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}