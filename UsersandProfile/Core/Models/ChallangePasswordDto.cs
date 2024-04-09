using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [JsonPropertyName("current_password")]
        public required string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [JsonPropertyName("confirmation_password")]
        public required string ConfirmationPassword { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [JsonPropertyName("new_password")]
        public required string NewPassword { get; set; }
    }