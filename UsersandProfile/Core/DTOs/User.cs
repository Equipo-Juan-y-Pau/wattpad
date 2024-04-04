// Models/Pedido.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Timers;

namespace ServicioUsers.DTOs
{
    
   public class RegisterDto
    {
        // Nombre con el cual el usuario es visible e inicia sesión
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [JsonPropertyName("username")]
        public required string Username { get; set; }

        // Correo electrónico del usuario
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es valido")]
        [JsonPropertyName("email")]
        public required string Email { get; set; }

        // Contraseña del usuario
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }

    public class LoginDto
    {
        // Correo electrónico del usuario
        [Required(ErrorMessage = "Campo obligatorio")]
        [JsonPropertyName("username_email")]
        public required string UsernameOrEmail { get; set; }

        // Contraseña del usuario
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }
}