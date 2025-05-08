using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Shared.models
{
    public class Utilizador
    {

        #region Atributos
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Nome { get; set; }
       

        public int  EmpresaId { get; set; }
       
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        #endregion
    }
}
