using App.Domain.Abstractions.CommandRepo.Base;
using App.Domain.Entities;


namespace App.Domain.Abstractions.CommandRepo
{
    public interface ISubCategoryCommandRepository : ICommandRepository<SubCategory>
    {
        Task<bool> UpdateAssainFormMapping(string SubCategoryId, List<string> SentenceFormId);
        
    }
}
