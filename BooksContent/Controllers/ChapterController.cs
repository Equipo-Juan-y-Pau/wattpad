using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BooksContent.Models;
//using BooksContent.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersandChapter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        // GET: Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chapter>>> GetChapters()
        {
            var chapters = await _chapterService.GetChapters(); // Aquí también ajusta el nombre del método
            return Ok(chapters);
        }

        // GET: Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chapter>> GetChapterByID(int id)
        {
            var chapter = await _chapterService.GetChapterByID(id);

            if (chapter == null)
            {
                return NotFound($"No se encontró un perfil con el ID {id}.");
            }

            return Ok(chapter);
        }

        // POST: Chapters
        [HttpPost]
        public async Task<ActionResult<Chapter>> CreateChapter([FromBody] Chapter chapter)
        {
            if (chapter == null)
            {
                return BadRequest("El perfil proporcionado es nulo.");
            }
            try
            {
                var createdChapter = await _chapterService.CreateChapter(chapter);
                if (createdChapter == null)
                {
                    return NotFound("No se pudo crear el perfil.");
                }

                return CreatedAtAction(nameof(GetChapterByID), new { id = createdChapter.Id }, createdChapter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            var wasDeleted = await _chapterService.DeleteChapter(id);

            if (!wasDeleted)
            {
                return NotFound($"No se encontró un perfil con el ID {id}.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapter(int id, Chapter chapter)
        {
            if (id != chapter.Id)
            {
                return BadRequest("El ID del perfil no coincide con el ID Proporcionado.");
            }

            var updatedChapter = await _chapterService.UpdateChapter(chapter);

            if (updatedChapter == null)
            {
                return NotFound($"No se encontró un perfil con el ID {id}.");
            }

            return Ok(updatedChapter);
        }

    }
}
