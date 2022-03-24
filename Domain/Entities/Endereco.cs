using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        [MaxLength(400, ErrorMessage = "Tamaho máximo do campo {0} é de {1} caracteres")]
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string EstadoUF { get; set; }
        [Required]
        public string CEP { get; set; }
    }
}
