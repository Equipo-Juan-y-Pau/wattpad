// Models/Pedido.cs
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UsersandProfile.Models
{
    
    [Table("profiles")]
    public class Profile
    {
        [Column("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("nombre")]
        [JsonPropertyName("nombre")]
        public required string NombreCompleto{ get; set; }

        [Column("avatar_url")]
        [JsonPropertyName("avatar_url")]
        public string? AvatarURL { get; set; }

        [Column("foto_url")]
        [JsonPropertyName("foto_perfil_url")]
        public string? FotoPerfilURL { get; set; }
    }
}