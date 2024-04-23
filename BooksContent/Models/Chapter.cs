// Models/Pedido.cs
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BooksContent.Models
{
    [Table("chapters")]
    public class Chapter
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public required string Title{ get; set; }

        [Column("numero")]
        public string? AvatarURL { get; set; }

        [Column("fecha_registro")]
        public string? FechaRegistro { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        [Column("book_id")]
        public string? BookId { get; set; }
    }
}