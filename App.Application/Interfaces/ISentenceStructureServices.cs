using App.Application.DTOs;
using App.Application.Features.SentenceStructureFeatures.CommandHandlers;


namespace App.Application.Interfaces
{
    public interface ISentenceStructureServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateSentenceStructureCommand entity);
        Task<IEnumerable<SentenceStructureDTOs>> GetAllAsync();
        Task<SentenceStructureDTOs> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateSentenceStructureCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
    }
}
