﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Setor
    {
        [Key]
        public int SetorId { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        [Required]
        public string UsuarioCriacao { get; set; }
    }
}
