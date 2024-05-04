using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BooksContent.Models
{
    public class Chapter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Para manejar el ID como string en la aplicación
        [Required]
        public string? Id { get; set; }

        [BsonElement("title")]
        [Required]
        public string? Title { get; set; }

        [BsonElement("number")]
        [Required]
        public int Number { get; set; }  // Asumiendo que quieres un número de capítulo

        [BsonElement("publishedDate")]
        [Required]
        public DateTime? PublishedDate { get; set; }  // Asumiendo que tienes una fecha de publicación

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("bookId")]
        [Required]
        [BsonRepresentation(BsonType.ObjectId)] // Si bookId también es un ObjectId
        public string? BookId { get; set; }
    }
}
