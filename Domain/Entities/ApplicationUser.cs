﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }
        public string Status { get; set; }
        [Display (Name = "Data de criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Último Login")]
        public DateTime UltimoLogin { get; set; }
        public int? AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
        public int? FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
