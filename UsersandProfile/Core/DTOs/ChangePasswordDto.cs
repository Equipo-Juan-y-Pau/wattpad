using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UsersandProfile.Models {
    /// <summary>
/// DTO para el cambio de contraseña de un usuario.
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// La contraseña actual del usuario.
    /// </summary>
    /// <remarks>
    /// Esto se utiliza para verificar la identidad del usuario antes de permitir cambiar la contraseña.
    /// </remarks>
    [Required(ErrorMessage = "El campo 'contraseña actual' es obligatorio.")]
    [MinLength(6, ErrorMessage = "La contraseña actual debe tener al menos 6 caracteres.")]
    [JsonPropertyName("current_password")]
    public string? CurrentPassword { get; set; }

    /// <summary>
    /// La confirmación de la contraseña nueva.
    /// </summary>
    /// <remarks>
    /// La contraseña de confirmación debe coincidir con la nueva contraseña para completar el cambio.
    /// </remarks>
    [Required(ErrorMessage = "El campo 'confirmación de contraseña' es obligatorio.")]
    [MinLength(6, ErrorMessage = "La confirmación de contraseña debe tener al menos 6 caracteres.")]
    [JsonPropertyName("confirmation_password")]
    public string? ConfirmationPassword { get; set; }

    /// <summary>
    /// La nueva contraseña que el usuario desea establecer.
    /// </summary>
    /// <remarks>
    /// Asegúrese de que la nueva contraseña sea diferente de la actual para mantener la seguridad de la cuenta.
    /// </remarks>
    [Required(ErrorMessage = "El campo 'nueva contraseña' es obligatorio.")]
    [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres.")]
    [JsonPropertyName("new_password")]
    public string? NewPassword { get; set; }
}

}
