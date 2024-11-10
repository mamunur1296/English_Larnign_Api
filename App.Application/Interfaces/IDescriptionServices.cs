using App.Application.DTOs;
using App.Application.Features.DescriptionFeatures.CommandHandlers;


namespace App.Application.Interfaces
{
    public interface IDescriptionServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateDescriptionCommand entity);
        Task<IEnumerable<DescriptionDTOs>> GetAllAsync();
        Task<DescriptionDTOs> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateDescriptionCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
    }
}
