using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BooksContent.DTOs
{
    public class Chapter
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public int Number { get; set; }  

        public string? Content { get; set; }

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? BookId { get; set; }
    }
}
