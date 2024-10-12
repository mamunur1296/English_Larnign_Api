using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Implementation.Query
{
    public class SentenceFormsQueryRepository : QueryRepository<SentenceForms>, ISentenceFormsQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SentenceFormsQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<SentenceForms>> GetAllSentenceFormsWithStructure()
        {
            var sentenceForms = await _applicationDbContext.SentenceFormss
                .Include(sc => sc.SentenceFormStructureMapping)
                .ThenInclude(scfm => scfm.SentenceStructure)
                .ToListAsync();

            return sentenceForms;
        }
        
    }
}
