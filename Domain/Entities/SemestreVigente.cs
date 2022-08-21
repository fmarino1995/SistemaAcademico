using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SemestreVigente
    {
        [Key]
        public int SemestreVigenteId { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }
        public bool Vigente { get; set; }
    }
}
