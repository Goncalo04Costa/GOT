using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.models;
using System.Collections.Generic;

namespace GOTinforcavado.Services
{
    public class EventoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/evento";

        public EventoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Evento> CreateEventoAsync(Evento evento)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, evento);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao criar evento. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            var eventoResponse = await response.Content.ReadFromJsonAsync<Evento>();

            if (eventoResponse == null)
            {
                throw new Exception("Erro ao processar a resposta da API. O evento retornado é nulo.");
            }

            return eventoResponse;
        }

        public async Task<List<Evento>> GetEventosByTicketIdAsync(string ticketId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/ticket/{ticketId}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao obter eventos para o TicketId {ticketId}. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            var comentarios = await response.Content.ReadFromJsonAsync<List<Evento>>();

            if (comentarios == null)
            {
                throw new Exception($"Erro ao processar a resposta da API. A lista de eventos para o TicketId {ticketId} é nula.");
            }

            return comentarios;
        }
        public async Task<List<Evento>> GetEventosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Evento>>(BaseUrl);
        }

        public async Task<Evento> UpdateEventoAsync(int id, Evento updatedEvento)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", updatedEvento);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Evento>();
        }

        public async Task DeleteEventoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}