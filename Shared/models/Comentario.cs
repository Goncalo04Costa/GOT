using Shared.models;
using System.ComponentModel.DataAnnotations.Schema;

public class Comentario
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string Conteudo { get; set; }

    [ForeignKey("Ticket")]
    public int TicketId { get; set; }
    public virtual Ticket Ticket { get; set; }
    public int UtilizadorId { get; set; }

    public bool publico { get; set; }


    public virtual Utilizador Utilizador { get; set; }
}
