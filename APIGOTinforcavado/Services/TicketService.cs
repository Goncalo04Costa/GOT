namespace APIGOTinforcavado.Services
{
    using APIGOTinforcavado.Repositories;
    using Shared.models;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class TicketService
    {
        private readonly TicketRepository _ticketRepository;
        private readonly EmailSender _emailSender;

        public TicketService(TicketRepository ticketRepository, EmailSender emailSender)
        {
            _ticketRepository = ticketRepository;
            _emailSender = emailSender;
        }

       
        private static string GenerateFixedRandomId(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input + DateTime.UtcNow.Ticks));
                return BitConverter.ToString(hashBytes.Take(8).ToArray()).Replace("-", "").ToLower();
            }
        }

      
        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));

            ticket.Id = GenerateFixedRandomId(ticket.Nome);
            ticket.EstadoTarefa = EstadoTarefa.PorIniciar;
            ticket.Data = DateTime.UtcNow;

            try
            {
                var createdTicket = await _ticketRepository.CreateAsync(ticket);

                
                string subject = "Confirmação de Criação de Ticket";
                string body = $"O seu ticket foi criado com sucesso!\n\nCódigo do Ticket: {createdTicket.Id}\n\nAcompanhe o seu ticket em:\nhttps://localhost:7026/AcompanhaTicket?codigo={createdTicket.Id}";
                await _emailSender.SendEmailAsync(ticket.Email, subject, body);

                return createdTicket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar ticket.", ex);
            }
        }

    
        public async Task<Ticket> GetTicketByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("ID do ticket não pode ser nulo ou vazio.", nameof(id));

            try
            {
                return await _ticketRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar ticket com ID {id}.", ex);
            }
        }

        
        public async Task<Ticket> UpdateTicketStatusAsync(string id, EstadoTarefa status)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("ID do ticket não pode ser nulo ou vazio.", nameof(id));

            try
            {
                var ticket = await _ticketRepository.GetByIdAsync(id);
                if (ticket == null) return null;

                ticket.EstadoTarefa = status;
                await _ticketRepository.UpdateAsync(ticket);

                return ticket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar status do ticket com ID {id}.", ex);
            }
        }

      
        public async Task<List<Ticket>> GetTicketsAsync()
        {
            try
            {
                return await _ticketRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao procurar tickets.", ex);
            }
        }

      
        public async Task<List<Ticket>> SearchTicketsByCodeAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return new List<Ticket>();

            try
            {
                var tickets = await _ticketRepository.GetAllAsync();
                return tickets.Where(t => t.Id.Equals(codigo, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar tickets pelo código {codigo}.", ex);
            }
        }

        public async Task<Ticket> UpdateTicketAsync(string id, Ticket updatedTicket)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("ID do ticket não pode ser nulo ou vazio.", nameof(id));

            try
            {
                var existingTicket = await _ticketRepository.GetByIdAsync(id);
                if (existingTicket == null) return null;

                existingTicket.Nome = updatedTicket.Nome;
                existingTicket.Empresa = updatedTicket.Empresa;
                existingTicket.Email = updatedTicket.Email;
                existingTicket.Mensagem = updatedTicket.Mensagem;
                existingTicket.EscolhaInicial = updatedTicket.EscolhaInicial;
                existingTicket.Departamento = updatedTicket.Departamento;
                existingTicket.EstadoTarefa = updatedTicket.EstadoTarefa;
                existingTicket.Comentarios = updatedTicket.Comentarios;
                existingTicket.Ficheiros = updatedTicket.Ficheiros;

                await _ticketRepository.UpdateAsync(existingTicket);
                return existingTicket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar ticket com ID {id}.", ex);
            }
        }

    
        public async Task UploadFilesAsync(string ticketId, List<IFormFile> ficheiros)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket == null)
                return;

            foreach (var ficheiro in ficheiros)
            {
                using var memoryStream = new MemoryStream();
                await ficheiro.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                var uploadedFile = new UploadedFiles
                {
                    NomeFicheiro = ficheiro.FileName,
                    FileData = fileBytes,
                    FileType = ficheiro.ContentType,
                    TicketId = ticketId
                };

                ticket.Ficheiros.Add(uploadedFile);
            }

            await UpdateTicketAsync(ticketId, ticket);
        }

   
        public async Task<UploadedFiles> GetFileByIdAsync(int fileId)
        {
            try
            {
                var file = await _ticketRepository.GetFileByIdAsync(fileId); 
                if (file == null)
                    throw new InvalidOperationException("Ficheiro não encontrado.");

                return file;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar ficheiro com ID {fileId}.", ex);
            }
        }

    }
}
