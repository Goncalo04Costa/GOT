using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.models;
using System.Collections.Generic;

namespace GOTinforcavado.Services
{
    public class ComentarioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/comentario";

        public ComentarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ComentarioTicket> CreateComentarioAsync(ComentarioTicket comentario)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, comentario);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao criar comentário. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            var comentarioResponse = await response.Content.ReadFromJsonAsync<ComentarioTicket>();

            if (comentarioResponse == null)
            {
                throw new Exception("Erro ao processar a resposta da API. O comentário retornado é nulo.");
            }

            return comentarioResponse;
        }

        public async Task<ComentarioTicket> GetComentarioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ComentarioTicket>($"{BaseUrl}/{id}");
        }

        public async Task<List<ComentarioTicket>> GetComentariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ComentarioTicket>>(BaseUrl);
        }

        public async Task<ComentarioTicket> UpdateComentarioAsync(int id, ComentarioTicket updatedComentario)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", updatedComentario);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ComentarioTicket>();
        }

        public async Task<List<ComentarioTicket>> GetComentariosByTicketIdAsync(string ticketId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/ticket/{ticketId}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
            }

            var comentarios = await response.Content.ReadFromJsonAsync<List<ComentarioTicket>>();

            if (comentarios == null)
            {
                throw new Exception($"Erro ao processar a resposta da API. A lista de comentários para o TicketId {ticketId} é nula.");
            }

            return comentarios;
        }

        public async Task<List<ComentarioTicket>> GetComentariosPorCodigoTicketAsync(string codigoTicket)
        {
            try
            {
                // Fazendo a requisição HTTP ao backend passando o código do ticket
                var response = await _httpClient.GetAsync($"{BaseUrl}/ticket/codigo/{codigoTicket}");

                // Se a resposta não for bem-sucedida, lance uma exceção com a mensagem de erro
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao buscar comentários. Código: {response.StatusCode}, Mensagem: {errorMessage}");
                }

                // Se a resposta for bem-sucedida, leia o conteúdo JSON e converta em uma lista de Comentarios
                var comentarios = await response.Content.ReadFromJsonAsync<List<ComentarioTicket>>();

                if (comentarios == null)
                {
                    throw new Exception($"Erro ao processar a resposta da API. A lista de comentários para o código {codigoTicket} é nula.");
                }

                return comentarios;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar comentários para o ticket com código {codigoTicket}.", ex);
            }
        }

        public async Task DeleteComentarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
