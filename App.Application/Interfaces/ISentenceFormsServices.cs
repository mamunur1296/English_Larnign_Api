using App.Application.DTOs;
using App.Application.Features.SentenceFormsFeatures.CommandHandlers;

namespace App.Application.Interfaces
{
    public interface ISentenceFormsServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateSentenceFormsCommand entity);
        Task<IEnumerable<SentenceFormsDTOs>> GetAllAsync();
        Task<SentenceFormsDTOs> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateSentenceFormsCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
        Task<bool> AssainStructure(string formateID, List<string> structureIDs);
    }
}
