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
           
            var token = await _localStorageService.GetItemAsync<string>("authToken");

           
            if (string.IsNullOrEmpty(token))
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(anonymous); 
            }

            
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);

           
            var claims = jwtToken.Claims.ToList();

           
            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

       
        public void NotifyUserAuthentication(string token, string nome, string email)
        {
         
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)  
            };

            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            var authState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(authState)); 
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
