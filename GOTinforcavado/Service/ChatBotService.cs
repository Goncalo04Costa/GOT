using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.models;

namespace GOTinforcavado.Services
{
    public class ChatBotService
    {
        private readonly HttpClient _httpClient;

        public ChatBotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       
        public async Task<string> ObterRespostaAsync(string mensagemUsuario)
        {
            var resposta = await _httpClient.PostAsJsonAsync("api/ChatBotMessages/responder", mensagemUsuario);

            if (resposta.IsSuccessStatusCode)
            {
                var respostaContent = await resposta.Content.ReadFromJsonAsync<ResponseMessage>();
                return respostaContent?.Resposta ?? "Desculpe, não consegui entender sua pergunta.";
            }

            return "Erro ao obter resposta.";
        }

 
        public async Task CriarMensagemAsync(ChatBotMessages mensagem)
        {
            var resposta = await _httpClient.PostAsJsonAsync("api/ChatBotMessages", mensagem);

            if (!resposta.IsSuccessStatusCode)
            {
            
                throw new InvalidOperationException("Erro ao criar mensagem.");
            }
        }
    }

    public class ResponseMessage
    {
        public string Resposta { get; set; }
    }
}
