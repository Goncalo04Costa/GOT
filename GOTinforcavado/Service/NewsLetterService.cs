using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.models;
using System.Collections.Generic;

namespace GOTinforcavado.Services
{ 
    public class NewsLetterService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/NewsLetter"; 

        public NewsLetterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para obter todos os emails da NewsLetters
        public async Task<List<NewsLetter>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NewsLetter>>(BaseUrl);
        }

        // Método para adicionar um novo email a  NewsLetter
        public async Task AddAsync(NewsLetter newsLetter)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, newsLetter);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao adicionar NewsLetter. Código: {response.StatusCode}, Mensagem: {errorMessage}");
            }
        }

        // Método para excluir um email da Newsletter
        public async Task<bool> DeleteAsync(string email)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{email}");

            return response.IsSuccessStatusCode;
        }
    }
}
