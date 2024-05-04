using System.ComponentModel.DataAnnotations;

namespace UsersandProfile.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) para las operaciones de usuario que requieren nombre de usuario, email y contraseña.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// El nombre de usuario único que identifica al usuario.
        /// </summary>
        /// <example>johndoe</example>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string? Username { get; set; }

        /// <summary>
        /// La dirección de correo electrónico del usuario.
        /// </summary>
        /// <example>johndoe@example.com</example>
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        public string? Email { get; set; }

        /// <summary>
        /// La contraseña para acceder a la cuenta de usuario.
        /// </summary>
        /// <example>Pass#1234</example>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string? Password { get; set; }
    }

    /// <summary>
    /// DTO (Data Transfer Object) para las operaciones de Login.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// El nombre de usuario o correo electrónico del usuario.
        /// </summary>
        /// <example>johndoe o johndoe@example.com</example>
        [Required(ErrorMessage = "El identificador es obligatorio.")]
        public string? Identifier { get; set; }

        /// <summary>
        /// La contraseña para acceder a la cuenta de usuario.
        /// </summary>
        /// <example>Pass#1234</example>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string? Password { get; set; }
    }
}
