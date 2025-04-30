using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Shared.models
{
    public class Utilizador
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Login {  get; set; }
       
        public List<ComentarioTicket> Comentarios { get; set; } = new List<ComentarioTicket>();

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}
