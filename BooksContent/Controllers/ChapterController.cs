using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BooksContent.Models;
//using BooksContent.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<ActionResult<IEnumerable<Chapter>>> GetChapters(string bookId)
        {
            var chapters = await _chapterService.GetChaptersAsync(bookId); 
            
            if (chapters == null || chapters.Count() == 0)
            {
                return NotFound($"No se encontraron capitulos para el bookID: {bookId}");
            }

            return Ok(chapters);
        }

        // GET: books/{bookId}/chapters/{chapterId}
        [HttpGet("{chapterId}")]
        public async Task<ActionResult<Chapter>> GetChapterById(string bookId, string chapterId)
        {
            var chapter = await _chapterService.GetChapterByIDAsync(bookId, chapterId);

            if (chapter == null)
            {
                return NotFound($"No se encontro el capitulo con id: {chapterId} para el libro con bookId: {bookId}");
            }

            return Ok(chapter);
        }

        [HttpPost]
        public async Task<ActionResult<Chapter>> CreateProfile([FromBody] Chapter chapter)
        {
            if (chapter == null)
            {
                return BadRequest("Los datos del capitulo son nulos.");
            }

            try
            {
                var createdChapter = await _chapterService.CreateChapterAsync(chapter);
                if (createdChapter == null)
                {
                    return NotFound("No se pudo crear el capitulo.");
                }

                return CreatedAtAction(nameof(GetChapterById), new { id = createdChapter.Id }, createdChapter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al crear el capitulo: {ex}");
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
                    return NotFound($"Capitulo con ID: {id} no encontrado.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al eliminar el perfil con ID: {id}, {ex}");
            }
        }

    }
}
