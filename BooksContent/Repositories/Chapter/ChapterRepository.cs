using BooksContent.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksContent.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly IMongoCollection<Chapter> _chapters;

        public ChapterRepository(IMongoDatabase database)
        {
            _chapters = database.GetCollection<Chapter>("Chapters");
        }

        public async Task<IEnumerable<Chapter>> GetChaptersAsync()
        {
            return await _chapters.Find(_ => true).ToListAsync();
        }

        public async Task<Chapter?> GetChapterByIDAsync(string id, string bookId)
        {
            var filter = Builders<Chapter>.Filter.Eq(c => c.Id, id) & Builders<Chapter>.Filter.Eq(c => c.BookId, bookId);
            return await _chapters.Find(chapter => chapter.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Chapter> CreateChapterAsync(Chapter chapter)
        {
            await _chapters.InsertOneAsync(chapter);
            return chapter;
        }

        public async Task<bool> DeleteChapterAsync(string id)
        {
            var result = await _chapters.DeleteOneAsync(chapter => chapter.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> UpdateChapterAsync(string id, string bookId, Chapter chapter)
        {
            var filter = Builders<Chapter>.Filter.Eq(c => c.Id, id) & Builders<Chapter>.Filter.Eq(c => c.BookId, bookId);
            var update = Builders<Chapter>.Update
                .Set(c => c.Title, chapter.Title)
                .Set(c => c.Number, chapter.Number)
                .Set(c => c.PublishedDate, chapter.PublishedDate)
                .Set(c => c.Content, chapter.Content);
            
            var result = await _chapters.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

    }

    
}
