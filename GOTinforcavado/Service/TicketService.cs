using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace GOTinforcavado.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Ticket";

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Criar um novo Ticket
        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, ticket);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao criar ticket. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            var ticketResponse = await response.Content.ReadFromJsonAsync<Ticket>();

            if (ticketResponse == null)
            {
                throw new Exception("Erro ao processar a resposta da API. O ticket retornado é nulo.");
            }

            return ticketResponse;
        }

        // procura um Ticket pelo codigo
        public async Task<Ticket?> GetTicketByIdAsync(string codigo)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{codigo}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Ticket>();
            }


            return null;
        }

        // Obter todos os Tickets
        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>(BaseUrl);
        }

        // Atualizar um Ticket
        public async Task<Ticket> UpdateTicketAsync(string id, Ticket updatedTicket)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", updatedTicket);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao atualizar ticket. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            return await response.Content.ReadFromJsonAsync<Ticket>();
        }

        // Atualizar o Estado de um Ticket
        public async Task<Ticket> UpdateTicketStatusAsync(string id, EstadoTicket status)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{BaseUrl}/{id}/status", status);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao atualizar status do ticket. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            return await response.Content.ReadFromJsonAsync<Ticket>();
        }

        public async Task<Ticket?> SearchTicketByCodeAsync(string codigo)
        {
            var encodedCodigo = Uri.EscapeDataString(codigo);
            return await _httpClient.GetFromJsonAsync<Ticket>($"{BaseUrl}/search/{encodedCodigo}");
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
                    TicketId = int.Parse(ticketId)

                };

                ticket.Ficheiros.Add(uploadedFile);
            }

            await UpdateTicketAsync(ticketId, ticket);
        }

    }
}
