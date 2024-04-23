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

        public async Task<Chapter?> GetChapterByIDAsync(int id)
        {
            return await _chapters.Find(chapter => chapter.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Chapter> CreateChapterAsync(Chapter chapter)
        {
            await _chapters.InsertOneAsync(chapter);
            return chapter;
        }

        public async Task<bool> DeleteChapterAsync(int id)
        {
            var result = await _chapters.DeleteOneAsync(chapter => chapter.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
