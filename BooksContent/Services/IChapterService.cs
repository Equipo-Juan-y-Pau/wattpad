using System.Collections.Generic;
using System.Threading.Tasks; 
using BooksContent.Models; 
public interface IChapterService
{
    Task<IEnumerable<Chapter>> GetChaptersAsync();
    Task<Chapter?> GetChapterByIDAsync(int id);
    Task<Chapter> CreateChapterAsync(Chapter chapter);
    Task<bool> DeleteChapterAsync(int id);
    //Task<Chapter?> UpdateChapterAsync(Chapter chapterToUpdate);
}
