using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.models;
using System.Collections.Generic;
using System;

namespace GOTinforcavado.Services
{
    public class ComentarioService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Comentario";

        public ComentarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Criar novo comentário
        public async Task<Comentario> CreateComentarioAsync(Comentario comentario)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, comentario);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao criar comentário. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }

            var comentarioCriado = await response.Content.ReadFromJsonAsync<Comentario>();
            if (comentarioCriado == null)
                throw new Exception("Erro ao processar resposta. Comentário retornado é nulo.");

            return comentarioCriado;
        }

        // Buscar comentário por ID
        public async Task<Comentario?> GetComentarioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Comentario>($"{BaseUrl}/{id}");
        }

        // Buscar todos os comentários
        public async Task<List<Comentario>> GetAllComentariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Comentario>>(BaseUrl) ?? new List<Comentario>();
        }

        // Buscar comentários por ID do ticket
        public async Task<List<Comentario>> GetComentariosByTicketIdAsync(int ticketId)
        {
            return await _httpClient.GetFromJsonAsync<List<Comentario>>($"{BaseUrl}/por-ticket/{ticketId}") ?? new List<Comentario>();
        }

        // Apagar comentário
        public async Task<bool> DeleteComentarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
