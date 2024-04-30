using System.Collections.Generic;
using System.Threading.Tasks; 
using BooksContent.Models; 
public interface IChapterRepository
{
    Task<IEnumerable<Chapter>> GetChaptersAsync();
    Task<Chapter?> GetChapterByIDAsync(string id, string bookId);
    Task<Chapter> CreateChapterAsync(Chapter chapter);
    Task<bool> DeleteChapterAsync(string id);
    //Task<Chapter?> UpdateChapterAsync(Chapter chapterToUpdate);
}
