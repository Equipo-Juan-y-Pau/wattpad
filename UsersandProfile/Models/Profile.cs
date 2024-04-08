using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UsersandProfile.Models
{
	[Table("profiles")]
	public class Profile
	{
		[Column("id")]
		[JsonPropertyName("id")]
		public int Id {  get; set; }

		[Column("nombre")]
		[JsonPropertyName("nombre")]
		public string? Nombre { get; set; }

		[Column("avatar_url")]
        [JsonPropertyName("avatar")]
        public string? AvatarURL { get; set; }

        [Column("foto_url")]
        [JsonPropertyName("foto")]
        public string? FotoPerfilURL { get; set; }
	}
}
