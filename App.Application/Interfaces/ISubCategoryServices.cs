using App.Application.DTOs;
using App.Application.Features.SubCategoryFeatures.CommandHandlers;

namespace App.Application.Interfaces
{
    public interface ISubCategoryServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateSubCategoryCommand entity);
        Task<IEnumerable<SubCategoryDTOs>> GetAllAsync();
        Task<SubCategoryDTOs> GetByIdAsync(string id);
        Task<SearchBySubCategoryDTOs> SearchBySubCategory(string dubCategoryId,string verbId);
        Task<(bool Success, string id)> UpdateAsync(UpdateSubCategoryCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
        Task<bool> AssainForms(string SubCategoryId,List<string> SentenceFormId);
    }
}
