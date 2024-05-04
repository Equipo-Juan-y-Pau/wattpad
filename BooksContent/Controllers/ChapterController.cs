using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BooksContent.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BooksContent.Models;
using BooksContent.Services;

namespace BooksContent.Controllers
{
    [ApiController]
    [Route("books/{bookId}/chapters")] 
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        // GET: books/{bookId}/chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterDTO>>> GetChapters(string bookId)
        {
            var chapters = await _chapterService.GetChaptersAsync(bookId); 
            
            if (chapters == null || !chapters.Any())
            {
                return NotFound($"No se encontraron capítulos para el bookID: {bookId}");
            }

            var chapterDTOs = chapters.Select(chapter => MapToDTO(chapter));
            return Ok(chapterDTOs);
        }

        // GET: books/{bookId}/chapters/{chapterId}
        [HttpGet("{chapterId}")]
        public async Task<ActionResult<ChapterDTO>> GetChapterById(string bookId, string chapterId)
        {
            var chapter = await _chapterService.GetChapterByIDAsync(chapterId, bookId);

            if (chapter == null)
            {
                return NotFound($"No se encontró el capítulo con id: {chapterId} para el libro con bookId: {bookId}");
            }

            var chapterDTO = MapToDTO(chapter);
            return Ok(chapterDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ChapterDTO>> CreateChapter([FromBody] ChapterDTO chapterDTO)
        {
            if (chapterDTO == null)
            {
                return BadRequest("Los datos del capítulo son nulos.");
            }

            try
            {
                var chapter = MapFromDTO(chapterDTO);
                var createdChapter = await _chapterService.CreateChapterAsync(chapter);
                if (createdChapter == null)
                {
                    return NotFound("No se pudo crear el capítulo.");
                }

                var createdChapterDTO = MapToDTO(createdChapter);
                return CreatedAtAction(nameof(GetChapterById), new { bookId = createdChapterDTO.BookId, chapterId = createdChapter.Id }, createdChapterDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al crear el capítulo: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            try
            {
                var wasDeleted = await _chapterService.DeleteChapterAsync(id);
                if (!wasDeleted)
                {
                    return NotFound($"Capítulo con ID: {id} no encontrado.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al eliminar el capítulo con ID: {id}, {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter(string bookId, string id, [FromBody] ChapterDTO chapterDTO)
        {
            if (chapterDTO == null || bookId != chapterDTO.BookId)
            {
                return BadRequest("Los datos del capítulo no son válidos.");
            }

            try
            {
                var chapter = MapFromDTO(chapterDTO);
                var updated = await _chapterService.UpdateChapterAsync(id, bookId, chapter);
                if (!updated)
                {
                    return NotFound($"No se encontró el capítulo con ID: {id} para actualizar.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al actualizar el capítulo con ID: {id}, {ex}");
            }
        }

        // Método para mapear Chapter a ChapterDTO
        private ChapterDTO MapToDTO(Chapter chapter)
        {
            return new ChapterDTO
            {
                Title = chapter.Title,
                Number = chapter.Number,
                Content = chapter.Content,
                BookId = chapter.BookId
            };
        }

        // Método para mapear ChapterDTO a Chapter
        private Chapter MapFromDTO(ChapterDTO chapterDTO)
        {
            return new Chapter
            {
                Title = chapterDTO.Title,
                Number = chapterDTO.Number,
                Content = chapterDTO.Content,
                BookId = chapterDTO.BookId
            };
        }
    }
}
