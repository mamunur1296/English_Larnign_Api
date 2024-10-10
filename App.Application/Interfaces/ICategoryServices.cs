using App.Application.DTOs;
using App.Application.Features.CategoryFeatures.CommandHandlers;


namespace App.Application.Interfaces
{
    public interface ICategoryServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateCategoryCommand entity);
        Task<IEnumerable<CategoryDTOs>> GetAllAsync();
        Task<CategoryDTOs> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateCategoryCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
    }
}
