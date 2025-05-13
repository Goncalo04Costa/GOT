using Shared.models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GOTinforcavado.Services
{
    public class UtilizadorService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Utilizador";  // URL base para os utilizadores
        private const string LoginUrl = "api/Utilizador/login";  // URL para o login

        public UtilizadorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para obter um utilizador pelo ID
        public async Task<Utilizador> GetUtilizadorByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Utilizador>($"{BaseUrl}/{id}");
            }
            catch (Exception ex)
            {
                // Você pode tratar erros aqui, como quando não encontra o utilizador
                throw new InvalidOperationException($"Erro ao obter o utilizador com ID {id}", ex);
            }
        }

        // Método para obter todos os utilizadores
        public async Task<List<Utilizador>> GetUtilizadoresAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Utilizador>>(BaseUrl);
            }
            catch (Exception ex)
            {
                // Tratamento de erros ao tentar obter a lista de utilizadores
                throw new InvalidOperationException("Erro ao obter a lista de utilizadores", ex);
            }
        }

        // Método para autenticar o utilizador
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

                // Verifica se a resposta foi bem-sucedida
                if (!response.IsSuccessStatusCode)
                {
                    return null;  // Retorna null caso a autenticação falhe
                }

                // Lê a resposta do token, se for bem-sucedido
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return tokenResponse?.Token;
            }
            catch (Exception ex)
            {
                // Adiciona tratamento de erro, caso ocorra algum problema na requisição
                throw new InvalidOperationException("Erro ao tentar autenticar o utilizador", ex);
            }
        }


        // Método para obter utilizadores por EmpresaId
        public async Task<List<Utilizador>> GetUtilizadoresPorEmpresaAsync(int empresaId)
        {
            try
            {
                var url = $"{BaseUrl}/empresa/{empresaId}";
                return await _httpClient.GetFromJsonAsync<List<Utilizador>>(url);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao obter utilizadores da empresa com ID {empresaId}", ex);
            }
        }
        public async Task<bool> AtualizarUtilizadorAsync(string emailAtual, string novoEmail, string nome, string password)
        {
            var updateRequest = new UpdateUtilizadorRequest
            {
                EmailAtual = emailAtual,
                NovoEmail = novoEmail,
                Nome = nome,
                Password = password
            };

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/update", updateRequest);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.Error.WriteLine($"Erro ao atualizar o utilizador: {response.StatusCode}, {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao atualizar o utilizador: {ex.Message}");
                throw new InvalidOperationException("Erro ao atualizar o utilizador", ex);
            }
        }


        public async Task<Utilizador> GetUtilizadorByEmailAsync(string email)
        {
            try
            {
                var url = $"{BaseUrl}/email/{email}";
                return await _httpClient.GetFromJsonAsync<Utilizador>(url);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao obter o utilizador com email {email}", ex);
            }
        }


    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }

    public class UpdateUtilizadorRequest
    {
        public string EmailAtual { get; set; }  
        public string NovoEmail { get; set; }    
        public string Nome { get; set; }        
        public string Password { get; set; }     
    }

}
