// Services/ProfileService.cs
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
                var profiles = await _chapterRepository.GetChaptersAsync();
                return profiles;
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

    }
}
