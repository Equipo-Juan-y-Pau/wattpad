using System.Collections.Generic;
using System.Threading.Tasks;
using BooksContent.Models;
using MongoDB.Driver;
using MongoDB.Bson;

public class BooksRepository : IBooksRepository
{
    private readonly IMongoCollection<Book> _booksCollection;

    public BooksRepository(IMongoDatabase database)
    {
        _booksCollection = database.GetCollection<Book>("books");
    }

    public async Task<IEnumerable<Book>> GetAll()
    {
        return await _booksCollection.Find(book => true).ToListAsync();
    }

    public async Task<Book> GetById(string id)
    {
        var objectId = new ObjectId(id);
        return await _booksCollection.Find(book => book.Id == objectId).FirstOrDefaultAsync();
    }

    public async Task<Book> Create(Book book)
    {
        await _booksCollection.InsertOneAsync(book);
        return book;
    }

    public async Task<bool> Update(string id, Book book)
    {
        var objectId = new ObjectId(id);
        var result = await _booksCollection.ReplaceOneAsync(b => b.Id == objectId, book);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        var objectId = new ObjectId(id);
        var result = await _booksCollection.DeleteOneAsync(book => book.Id == objectId);
        return result.DeletedCount > 0;
    }

    public async Task<Book> Update(Book book, string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<Book>.Filter.Eq(b => b.Id, objectId);
        var update = Builders<Book>.Update
            .Set(b => b.Titulo, book.Titulo)
            .Set(b => b.Id_Autor, book.Id_Autor)
            .Set(b => b.Description, book.Description)
            .Set(b => b.fechaInicio, book.fechaInicio);

        var options = new FindOneAndUpdateOptions<Book>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await _booksCollection.FindOneAndUpdateAsync(filter, update, options);
    }
}
