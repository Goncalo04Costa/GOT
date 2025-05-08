using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.models
{
    public enum EstadoTicket
    {
        [Display(Name = "Todos")] Todos = -1,
        [Display(Name = "Por Iniciar")] PorIniciar = 0,
        [Display(Name = "Em Curso")] EmCurso = 1,
        [Display(Name = "Em analise")] EmAnalise = 2,
        [Display(Name = "Pendente")] Pendente = 3,
        [Display(Name = "Resolvido")] Resolvido = 4,
        [Display(Name = "Fechado")] Fechado = 5,
    }

    public class Ticket
    {

        #region Atributos
        [Required]
        public int Id { get; set; }

        public string codigo { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime Data { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Empresa é obrigatório.")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Assunto é obrigatório.")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "O campo Mensagem é obrigatório.")]
        public string Mensagem { get; set; }

        public string Departamento { get; set; }
        public string TipoTicket { get; set; }

        public int? UtilizadorId { get; set; }
        public Utilizador Utilizador { get; set; } 
     
        public EstadoTicket Estadodoticket { get; set; } = EstadoTicket.PorIniciar;

        
        public List<UploadedFiles>? Ficheiros { get; set; }

        #endregion

        #region Construtores
        public Ticket()
        {
            Ficheiros = new List<UploadedFiles>();
        }
        #endregion

    }
}
