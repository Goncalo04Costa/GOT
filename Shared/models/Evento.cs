using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.models
{
    public class Evento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string evento { get; set; }

        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public int UtilizadorId { get; set; }

        public virtual Utilizador Utilizador { get; set; }
    }
}
