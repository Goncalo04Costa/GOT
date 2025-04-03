using Shared.models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GOTinforcavado.Services
{
    public class UtilizadorService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Utilizador";
        private const string LoginUrl = "api/Utilizador/login"; 

        public UtilizadorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<Utilizador> GetUtilizadorByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Utilizador>($"{BaseUrl}/{id}");
        }

        public async Task<List<Utilizador>> GetUtilizadoresAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Utilizador>>(BaseUrl);
        }

        public async Task<string> AutenticarAsync(string email, string password)
        {
    
            var loginRequest = new
            {
                Email = email,
                Password = password
            };

            try
            {
              
                var response = await _httpClient.PostAsJsonAsync(LoginUrl, loginRequest);

             
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

             
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                return tokenResponse?.Token;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

   
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
