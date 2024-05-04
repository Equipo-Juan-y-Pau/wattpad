using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksContent.Models;
using BooksContent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BooksContent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBooksService _booksService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BooksController(ILogger<BooksController> logger, IBooksService booksService, IHttpContextAccessor httpContextAccessor)
        {
            _booksService = booksService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            _logger.LogInformation("Entrando al controlador para obtener todos los libros.");
            var books = await _booksService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookById(string id)
        {
            var bookResponse = await _booksService.GetBooksByID(id);

            if (bookResponse.Data == null)
            {
                return NotFound(bookResponse.Message);
            }

            return Ok(bookResponse);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }
                
                var userId = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                
                if (userId == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "UserId no pudo ser obtenido.");
                }

                book.Id_Autor = userId; 

                var createdBookResponse = await _booksService.AddBook(book);

                if (!createdBookResponse.Success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, createdBookResponse.Message);
                }

                return CreatedAtRoute("GetBook", new { id = createdBookResponse.Data }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                var userId = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                
                if (userId == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "UserId no pudo ser obtenido.");
                }
                
                var deleted = await _booksService.DeleteBook(id, userId);

                if (!deleted.Success)
                {
                    return NotFound(deleted.Message);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<IActionResult> UpdateBook(string id, [FromBody] Book book)
{
    try
    {
        if (book == null)
        {
            return BadRequest("El cuerpo del libro no puede ser nulo.");
        }

        var userId = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        if (userId == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo obtener el UserId.");
        }

        var updatedBook = await _booksService.UpdateBook(book, id, userId);

        if (updatedBook == null || updatedBook.Data == null || !updatedBook.Success)
        {
            return NotFound(updatedBook?.Message ?? "El libro no pudo ser actualizado o encontrado.");
        }

        return CreatedAtRoute("GetBook", new { id = updatedBook.Data.Id }, book);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno: {ex.Message}");
    }
}

    }
}
