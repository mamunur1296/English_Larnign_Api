using App.Domain.Abstractions.QueryRepo.Base;
using App.Domain.Entities;


namespace App.Domain.Abstractions.QueryRepo
{
    public interface ISentenceFormsQueryRepository : IQueryRepository<SentenceForms>
    {
        Task<IEnumerable<SentenceForms>> GetAllSentenceFormsWithStructure();
    }
}
