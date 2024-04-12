using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UsersandProfile.Models

{
    [Table("users")]
    
    public class User
    {
        [Column("id")]
        [JsonPropertyName("id")]
        public int Id {  get; set; }

        [Column("username")]
        [JsonPropertyName("username")]
        public string? Username {  get; set; }

        [Column("email")]
        [JsonPropertyName("email")]
        public string? Email {  get; set; }

        [Column("password_hash")]
        [JsonPropertyName("password")]
        public string? Password {  get; set; }

        [Column("role")]
        [JsonPropertyName("rol")]
        public string? Role {  get; set; }

        [Column("registration_date")]
        [JsonPropertyName("date")]
        public DateTime Date {  get; set; }

        [Column("email_confirmation")]
        [JsonPropertyName("valid")]
        public bool  Valid {  get; set; }


    }
}