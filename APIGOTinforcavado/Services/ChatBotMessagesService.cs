using APIGOTinforcavado.Repositories;
using Shared.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Services
{
    public class ChatBotMessagesService
    {
        private readonly ChatBotMessagesRepository _repository;

        public ChatBotMessagesService(ChatBotMessagesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ChatBotMessages>> ObterTodasMensagensAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao obter todas as mensagens do chatbot.", ex);
            }
        }

        public async Task<ChatBotMessages> CriarMensagemAsync(ChatBotMessages mensagem)
        {
            if (mensagem == null)
                throw new ArgumentNullException(nameof(mensagem));

            try
            {
                return await _repository.CreateAsync(mensagem);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar mensagem do chatbot.", ex);
            }
        }

        public async Task<string?> ObterMensagemPorPalavrasChaveAsync(string inputUsuario)
        {
            var mensagens = await _repository.GetAllAsync(); // Buscar todas as mensagens/palavras-chave

            foreach (var msg in mensagens)
            {
                var palavras = msg.PalavrasChave.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var palavra in palavras)
                {
                    if (inputUsuario.Contains(palavra, StringComparison.OrdinalIgnoreCase))
                    {
                        return msg.Message; // Retorna a resposta associada à palavra-chave
                    }
                }
            }

            return null; // Nenhuma palavra-chave foi encontrada
        }


        public async Task<bool> ApagarMensagemAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.", nameof(id));

            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao apagar mensagem com ID {id}.", ex);
            }
        }
    }
}
