

using App.Domain.Abstractions.QueryRepo.Base;
using App.Domain.Entities;

namespace App.Domain.Abstractions.QueryRepo
{
    public interface ISentenceStructureQueryRepository : IQueryRepository<SentenceStructure>
    {
        Task<IEnumerable<SentenceStructure>> GetAllFilterBySubCatagoryIdAndFormsIdAsync(string subCatagoryID, string formsId, int pageSize, int pageNumber);
    }
}
