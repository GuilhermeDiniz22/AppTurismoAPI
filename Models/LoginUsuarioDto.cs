using System.ComponentModel.DataAnnotations;

namespace AppTurismoAPI.Models
{
    public class LoginUsuarioDto
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
