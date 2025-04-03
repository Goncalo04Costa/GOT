using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.models
{
    public class UploadedFiles
    {
        public int Id { get; set; }
        public string NomeFicheiro { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }

        [Required] 
        public DateTime Data { get; set; } = DateTime.UtcNow;

        public string? TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }

        public int? ComentarioId { get; set; }
        public virtual Comentario? Comentario { get; set; }
    }
}
