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

        public async Task<Comentario> CreateComentarioAsync(Comentario comentario)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, comentario);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao criar comentário. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            var comentarioResponse = await response.Content.ReadFromJsonAsync<Comentario>();

            if (comentarioResponse == null)
            {
                throw new Exception("Erro ao processar a resposta da API. O comentário retornado é nulo.");
            }

            return comentarioResponse;
        }

        public async Task<Comentario> GetComentarioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Comentario>($"{BaseUrl}/{id}");
        }

        public async Task<List<Comentario>> GetComentariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Comentario>>(BaseUrl);
        }

        public async Task<Comentario> UpdateComentarioAsync(int id, Comentario updatedComentario)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", updatedComentario);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Comentario>();
        }

        public async Task<List<Comentario>> GetComentariosByTicketIdAsync(string ticketId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/ticket/{ticketId}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
            }

            var comentarios = await response.Content.ReadFromJsonAsync<List<Comentario>>();

            if (comentarios == null)
            {
                throw new Exception($"Erro ao processar a resposta da API. A lista de comentários para o TicketId {ticketId} é nula.");
            }

            return comentarios;
        }

        public async Task DeleteComentarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
