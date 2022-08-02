﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Disciplina
    {
        [Key]
        public int DisplicinaId { get; set; }
        [Required]
        public string Nome { get; set; }
        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
        [Required]
        public string Turno { get; set; }
        [Required]
        public string Sigla { get; set; }
        [Required]
        public string Matricula { get; set; }
        public string PreRequisito { get; set; }
    }
}
