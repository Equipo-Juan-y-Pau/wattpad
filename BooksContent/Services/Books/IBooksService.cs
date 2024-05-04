using BooksContent.Models;

namespace BooksContent.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<ServiceResponse<String>> GetBooksByID(string id);
        Task<ServiceResponse<String>> AddBook(Book book);
        Task<ServiceResponse<String>> DeleteBook(string id, string userId);
        Task<ServiceResponse<String>> UpdateBook(Book book, string id, string userId);
    }
}