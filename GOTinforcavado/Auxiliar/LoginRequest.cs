using System.ComponentModel.DataAnnotations;

namespace GOTinforcavado.Auxiliar
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password é obrigatória.")]
        public string Password { get; set; }
    }
}
