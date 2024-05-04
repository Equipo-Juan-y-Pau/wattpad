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
        // Obtener el libro por su ID
        var bookResponse = await GetBooksByID(id);

        // Verificar si el libro no fue encontrado
        if (!bookResponse.Success || bookResponse.Data == null)
        {
            response.Message = $"El libro con ID {id} no fue encontrado";
            response.Success = false;
        }
        else
        {
            // Validar si el autor del libro coincide con el usuario
            var book = await _booksRepository.GetById(id); // Aquí estamos obteniendo el libro directamente

            if (book.Id_Autor != userId)
            {
                response.Message = $"El libro con ID {id} no te pertenece";
                response.Success = false;
            }
            else
            {
                // Eliminar el libro
                await _booksRepository.Delete(id);
                response.Success = true;
                response.Message = "El libro fue eliminado";
            }
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
        // Intentar obtener el libro existente
        var existingBook = await _booksRepository.GetById(id);

        if (existingBook == null)
        {
            response.Message = "El libro no existe";
            response.Success = false;
            return response; // Devolver la respuesta aquí para evitar un acceso nulo
        }

        if (existingBook.Id_Autor != userId)
        {
            response.Message = "No tienes permiso para actualizar este libro";
            response.Success = false;
            return response; // Devolver la respuesta para evitar la continuación
        }

        // Actualizar solo si los valores no son nulos o vacíos
        if (!string.IsNullOrWhiteSpace(book.Titulo))
        {
            existingBook.Titulo = book.Titulo;
        }
        if (!string.IsNullOrWhiteSpace(book.Description))
        {
            existingBook.Description = book.Description;
        }

        // `fechaInicio` es un valor de tipo `DateTime`, por lo tanto, no necesita una verificación nula
        existingBook.fechaInicio = book.fechaInicio;

        await _booksRepository.Update(existingBook, id);

        response.Success = true;
        response.Message = "El libro fue actualizado";
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
