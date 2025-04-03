using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.models
{
    public enum EstadoTarefa
    {
        [Display(Name = "Todos")] Todos = -1,
        [Display(Name = "Por Iniciar")] PorIniciar = 0, //#1923560D
        [Display(Name = "Em Curso")] EmCurso = 1, // #8BB7311A
        [Display(Name = "Concluído")] Concluido = 2, // #33A3631A
        [Display(Name = "Publicado")] Publicado = 3, // #5D22891A
        [Display(Name = "Arquivadas")] Arquivado = 4, // #192356
        [Display(Name = "Em espera")] EmEspera = 5, // #192356
    }

    public class Ticket
    {
        [Required] // O ID é obrigatório
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required] // Garante que a data seja sempre preenchida
        public DateTime Data { get; set; } = DateTime.UtcNow;

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Empresa { get; set; }

        [Required]
        [EmailAddress] 
        public string Email { get; set; }

        public string? Telefone { get; set; }

        [Required]
        public string Assunto { get; set; }

        [Required]
        public string Mensagem { get; set; }

        [Required]
        public string EscolhaInicial { get; set; }

        [Required]
        public string Departamento { get; set; }

        public EstadoTarefa EstadoTarefa { get; set; } = EstadoTarefa.PorIniciar;

        public List<Comentario>? Comentarios { get; set; }
        public List<UploadedFiles>? Ficheiros { get; set; }

        public Ticket()
        {
            Comentarios = new List<Comentario>();
            Ficheiros = new List<UploadedFiles>();
        }
    }
}
