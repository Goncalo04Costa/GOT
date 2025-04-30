namespace APIGOTinforcavado.Services
{
    using APIGOTinforcavado.Repositories;
    using Shared.models;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using static MudBlazor.Colors;

    public class TicketService
    {
        private readonly TicketRepository _ticketRepository;
        private readonly EmailSender _emailSender;

        public TicketService(TicketRepository ticketRepository, EmailSender emailSender)
        {
            _ticketRepository = ticketRepository;
            _emailSender = emailSender;
        }

        private static string GenerateFixedRandomCode(string input)
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

            ticket.codigo = GenerateFixedRandomCode(ticket.Nome);
            ticket.Estadodoticket = EstadoTicket.PorIniciar;
            ticket.Data = DateTime.UtcNow;

            try
            {
                var createdTicket = await _ticketRepository.CreateAsync(ticket);

                string subject = "Confirmação de Criação de Ticket";
                string body = $"O seu ticket foi criado com sucesso!\n\nCódigo do Ticket: {createdTicket.codigo}\n\nAcompanhe o seu ticket em:\nhttps://localhost:7026/AcompanhaTicket?codigo={createdTicket.codigo}";
                await _emailSender.SendEmailAsync(ticket.Email, subject, body);

                return createdTicket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar ticket.", ex);
            }
        }

        public async Task<Ticket> GetTicketByCodigoAsync(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentException("Código do ticket não pode ser nulo ou vazio.", nameof(codigo));

            try
            {
                return await _ticketRepository.GetByCodigoAsync(codigo); 
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar ticket com código {codigo}.", ex);
            }
        }

        public async Task<Ticket> GetTicketByIdAsync(int id) 
        {
            if (id <= 0)
                throw new ArgumentException("ID do ticket não pode ser nulo ou menor ou igual a zero.", nameof(id));

            try
            {
                return await _ticketRepository.GetByIdAsync(id); 
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar ticket com ID {id}.", ex);
            }
        }
 
        public async Task<Ticket> UpdateTicketStatusAsync(string codigo, EstadoTicket status) 
        {
            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentException("Código do ticket não pode ser nulo ou vazio.", nameof(codigo));

            try
            {
                var ticket = await _ticketRepository.GetByCodigoAsync(codigo); 
                if (ticket == null) return null;

                ticket.Estadodoticket = status;
                await _ticketRepository.UpdateAsync(ticket);

                return ticket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar status do ticket com código {codigo}.", ex);
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

  
                return tickets.Where(t => t.codigo == codigo).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar tickets pelo código {codigo}.", ex);
            }
        }


        public async Task<Ticket> UpdateTicketAsync(int id, Ticket updatedTicket) 
        {
            if (id <= 0) throw new ArgumentException("ID do ticket não pode ser nulo ou vazio.", nameof(id));

            try
            {
                var existingTicket = await _ticketRepository.GetByIdAsync(id); 
                if (existingTicket == null) return null;

                existingTicket.codigo = updatedTicket.codigo;
                existingTicket.Nome = updatedTicket.Nome;
                existingTicket.Empresa = updatedTicket.Empresa;
                existingTicket.Email = updatedTicket.Email;
                existingTicket.Mensagem = updatedTicket.Mensagem;
                existingTicket.TipoTicket = updatedTicket.TipoTicket;
                existingTicket.Departamento = updatedTicket.Departamento;
                existingTicket.Estadodoticket = updatedTicket.Estadodoticket;
                existingTicket.ComentarioTicket = updatedTicket.ComentarioTicket;
                existingTicket.Ficheiros = updatedTicket.Ficheiros;

                await _ticketRepository.UpdateAsync(existingTicket);
                return existingTicket;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar ticket com ID {id}.", ex);
            }
        }


        public async Task UploadFilesAsync(int ticketId, List<IFormFile> ficheiros)
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


                await _ticketRepository.AddFileToTicketAsync(uploadedFile);
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
