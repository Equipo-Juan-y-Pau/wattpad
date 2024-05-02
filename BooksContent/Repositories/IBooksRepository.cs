using System.Collections.Generic;
using System.Threading.Tasks;
using BooksContent.Models;

public interface IBooksRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book> GetById(string id);
    Task<Book> Create(Book book);
    Task<bool> Update(string id, Book book);
    Task<bool> Delete(string id);
   Task<Book> Update(Book book, string id);
}

