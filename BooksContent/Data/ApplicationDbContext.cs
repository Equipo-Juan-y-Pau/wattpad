using MongoDB.Driver;
using BooksContent.Models;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public IMongoCollection<Book> Books { get; }

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        Books = _database.GetCollection<Book>("books");
    }
}

