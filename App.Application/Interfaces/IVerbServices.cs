using App.Application.DTOs;
using App.Application.Features.VerbFeatures.CommandHandlers;

namespace App.Application.Interfaces
{
    public interface IVerbServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateVerbCommand entity);
        Task<IEnumerable<VerbDTOs>> GetAllAsync();
        Task<VerbDTOs> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateVerbCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
    }
}
