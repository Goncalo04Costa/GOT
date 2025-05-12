using APIGOTinforcavado.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using Shared.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Services
{
    public class UtilizadorService
    {
        private readonly UtilizadorRepository _utilizadorRepository;
        private readonly JwtGenerator _jwtGenerator;

        public UtilizadorService(UtilizadorRepository utilizadorRepository, JwtGenerator jwtGenerator)
        {
            _utilizadorRepository = utilizadorRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Utilizador> CreateUtilizadorAsync(Utilizador utilizador)
        {
            if (utilizador == null)
                throw new ArgumentNullException(nameof(utilizador));

            try
            {
                var existingUtilizador = await _utilizadorRepository.GetByEmailAsync(utilizador.Email);
                if (existingUtilizador != null)
                    throw new InvalidOperationException("Já existe um utilizador com este email.");

                // ⚠️ Futuramente: armazenar senha com hash
                return await _utilizadorRepository.CreateAsync(utilizador);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar o utilizador.", ex);
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var utilizador = await _utilizadorRepository.GetByEmailAsync(request.Email);
            if (utilizador == null)
                throw new UnauthorizedAccessException("Utilizador não encontrado.");

            
            if (request.Password != utilizador.Password)
                throw new UnauthorizedAccessException("Senha inválida.");

            var token = _jwtGenerator.GenerateJwtToken(utilizador);

            return new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(3)
            };
        }

        public async Task<Utilizador> GetUtilizadorByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.", nameof(id));

            try
            {
                return await _utilizadorRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar o utilizador com ID {id}.", ex);
            }
        }

        public async Task<List<Utilizador>> GetUtilizadoresAsync()
        {
            try
            {
                return await _utilizadorRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao procurar os utilizadores.", ex);
            }
        }

        public async Task<List<Utilizador>> GetUtilizadoresByEmpresaIdAsync(int empresaId)
        {
            return await _utilizadorRepository.GetByEmpresaIdAsync(empresaId);
        }


        public class LoginResponse
        {
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }
    }
}
