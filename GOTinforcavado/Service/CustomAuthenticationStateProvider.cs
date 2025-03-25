using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GOTinforcavado.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Tenta obter o token do localStorage
            var token = await _localStorageService.GetItemAsync<string>("authToken");

            // Se o token não for encontrado, o usuário é anônimo
            if (string.IsNullOrEmpty(token))
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(anonymous);  // Retorna um estado anônimo
            }

            // Se o token for encontrado, o usuário está autenticado
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);

            // Extraindo as claims do token
            var claims = jwtToken.Claims.ToList();

            // Crie um ClaimsPrincipal com as claims extraídas do token
            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        // Este método pode ser usado para notificar os componentes que o estado de autenticação mudou
        public void NotifyUserAuthentication(string token, string nome, string email)
        {
         
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)  
            };

            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            var authState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));  // Notifica a mudança de estado de autenticação
        }

        // Método para notificar que o usuário fez logout
        public void NotifyUserLogout()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = new AuthenticationState(anonymous);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));  
        }
    }
}
