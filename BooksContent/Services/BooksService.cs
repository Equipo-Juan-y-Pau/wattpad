using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksContent.Models;
using BooksContent.Services;

namespace BooksContent.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _booksRepository.GetAll();
        }

        public async Task<ServiceResponse<string>> GetBooksByID(string id)
        {
            var response = new ServiceResponse<String>();

            try 
            {
                var book = await _booksRepository.GetById(id);
                if (book == null)
                {
                    response.Success = false;
                    response.Message = "El libro no existe";
                }
                response.Success = true;
                response.Data = book;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Hubo un error: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> AddBook(Book book)
        {
            var response = new ServiceResponse<string>();

            try
            {
                book.fechaInicio = DateTime.Now;
                await _booksRepository.Create(book);
                response.Success = true;
                response.Data = book; // Modificado para devolver el ID del libro creado
                response.Message = "el libro fue creado";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Hubo un error: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteBook(string id, string userId)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var bookResponse = await GetBooksByID(id);

                if (!bookResponse.Success)
                {
                    response.Message = $"El libro con ID {id} no fue encontrado";
                    response.Success = false;
                }
                else if (bookResponse.Data.Id_Autor != userId)
                {
                    response.Message = $"El libro con ID {id} no te pertenece";
                    response.Success = false; 
                }
                else
                {
                    await _booksRepository.Delete(id);
                    response.Success = true;
                    response.Message = "El libro fue eliminado";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Hubo un error: {ex.Message}";
            }
            return response;
              
        }

        public async Task<ServiceResponse<string>> UpdateBook(Book book, string id, string userId)
        {
            var response = new ServiceResponse<string>();

            try 
            {
                var existingBook = await _booksRepository.GetById(id);

                if (existingBook == null)
                {
                    response.Message = "el libro no existe";
                    response.Success = false;
                }

                if (existingBook.Id_Autor != userId)
                {
                    response.Message = "no tienes permiso para actualizar este libro";
                    response.Success = false;
                }

                if (book.Titulo != null)
                {
                    existingBook.Titulo = book.Titulo;
                }
                if (book.Description != null)
                {
                    existingBook.Description = book.Description;
                }
                if (book.fechaInicio != null)
                {
                    existingBook.fechaInicio = book.fechaInicio;
                }
                await _booksRepository.Update(existingBook, id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Hubo un error: {ex.Message}";
            }
            return response;

        }

    }
}
