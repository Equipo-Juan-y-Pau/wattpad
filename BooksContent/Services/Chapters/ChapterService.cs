using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksContent.Models;
using Microsoft.Extensions.Logging;
using BooksContent.Repositories;

namespace UsersandProfile.Services
{
    public class ProfileService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly ILogger<ProfileService> _logger; 

        public ProfileService(IChapterRepository profileRepository, ILogger<ProfileService> logger)
        {
            _chapterRepository = profileRepository;
            _logger = logger; 
        }

        public async Task<IEnumerable<Chapter>> GetChaptersAsync(string bookId)
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los capitulos de la base de datos.");
                var chapters = await _chapterRepository.GetChaptersAsync();
                return chapters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fallo al obtener los capitulos.");
                throw;
            }
        }

        public async Task<Chapter?> GetChapterByIDAsync(string id, string bookId)
        {
            try
            {
                _logger.LogInformation($"Obtieniendo capitulos por ChapterID: {id}");
                return await _chapterRepository.GetChapterByIDAsync(id, bookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch profile with ID: {id}");
                throw;
            }
        }

        public async Task<Chapter> CreateChapterAsync(Chapter chapter)
        {
            try
            {
                _logger.LogInformation($"Creando un nuevo capitulo");
                chapter.PublishedDate = DateTime.UtcNow; // Llenar el campo PublishedDate con la fecha y hora actuales
                return await _chapterRepository.CreateChapterAsync(chapter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear el capitulo");
                throw;
            }
        }

        public async Task<bool> DeleteChapterAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Eliminando el capitulo con ID:{id}");
                return await _chapterRepository.DeleteChapterAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al intentar eliminar el capitulo");
                throw;
            }
        }

        public async Task<bool> UpdateChapterAsync(string id, string bookId, Chapter chapter)
        {
            try
            {
                _logger.LogInformation($"Actualizando el capitulo con ID:{id}");
                return await _chapterRepository.UpdateChapterAsync(id, bookId, chapter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al intentar actualizar el capitulo con ID:{id}");
                throw;
            }
        }

    }
}
