// Models/Pedido.cs
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Timers;

namespace ServicioUsers.Models
{
    
[Table("users")]
public class User
    {
        [Column("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("username")]
        [JsonPropertyName("username")]
        public required string Username { get; set; }

        [Column("email")]
        [JsonPropertyName("email")]
        public required string Email { get; set; }

        [Column("password_hash")]
        [JsonPropertyName("password_hash")]
        public required string PasswordHash { get; set; }

        [Column("role")]
        [JsonPropertyName("role")]
        public required string Role { get; set; }

        [Column("registration_date")]
        [JsonPropertyName("registration_date")]
        public required DateTime RegistrationDate { get; set; }

        [Column("email_confirmation")]
        [JsonPropertyName("email_confirmation")]
        public required bool EmailConfirmation { get; set; }
    }
}