using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SemestreVigente
    {
        [Key]
        public int SemestreVigenteId { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }
        public bool Vigente { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }


        [NotMapped]
        public DateTime? DataInicioAux { get; set; }
        [NotMapped]
        public DateTime? DataFimAux { get; set; }
    }
}