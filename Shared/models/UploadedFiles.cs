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

        #region Atributos
        public int Id { get; set; }
        public string NomeFicheiro { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }

        [Required] 
        public DateTime Data { get; set; } = DateTime.UtcNow;

        public int TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }

        #endregion
    }
}
