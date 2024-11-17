using App.Application.DTOs;
using App.Application.Features.SentenceStructureFeatures.CommandHandlers;
using Microsoft.AspNetCore.Http;


namespace App.Application.Interfaces
{
    public interface ISentenceStructureServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateSentenceStructureCommand entity);
        Task<bool> CreateFormXlsxAsync(IFormFile file);
        Task<IEnumerable<SentenceStructureDTOs>> GetAllAsync();
        Task<(IEnumerable<SentenceStructureDTOs>,int pageCount)> GetAllFilterAsync(string subCatagoryID, string formsId, int? pageSize, int? pageNumber);
        Task<SentenceStructureDTOs> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateSentenceStructureCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
        Task<bool> DeleteAll();
    }
}
