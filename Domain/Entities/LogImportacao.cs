using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class LogImportacao
    {
        [Key]
        public int LogImportacaoId { get; set; }
        [Required]
        public string TipoImportacao { get; set; }
        public DateTime DataCriacao { get; set; }
        [Required]
        public string Mensagem { get; set; }
        [Required]
        public string CaminhoArquivo { get; set; }
    }
}
