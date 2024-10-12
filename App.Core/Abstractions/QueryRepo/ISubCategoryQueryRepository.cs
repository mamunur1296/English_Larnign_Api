

using App.Domain.Abstractions.QueryRepo.Base;
using App.Domain.Entities;

namespace App.Domain.Abstractions.QueryRepo
{
    public interface ISubCategoryQueryRepository : IQueryRepository<SubCategory>
    {
        // Add specific Query methods here if needed
        Task<SubCategory> GetSubCategoryWithSentenceFormsAndStructures(string subCategoryId);
        Task<IEnumerable<SubCategory>> GetAllSubCategoryWithForms();  
    }
}
