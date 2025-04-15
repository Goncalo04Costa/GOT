using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.models
{

    public class Ticket
    {

        [Required]
        public int Id { get; set; }
       
        public string codigo { get; set; } = Guid.NewGuid().ToString();

        [Required] 
        public DateTime Data { get; set; } = DateTime.UtcNow;

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Empresa { get; set; }

        [EmailAddress] 
        public string Email { get; set; }

        public string Telefone { get; set; }

        [Required]
        public string Assunto { get; set; }

        [Required]
        public string Mensagem { get; set; }
     
        public string Departamento { get; set; }
        public string TipoTicket { get; set; }

        public int? UtilizadorId { get; set; } 

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
