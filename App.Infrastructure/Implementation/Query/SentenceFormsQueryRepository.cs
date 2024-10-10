using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;


namespace App.Infrastructure.Implementation.Query
{
    public class SentenceFormsQueryRepository : QueryRepository<SentenceForms>, ISentenceFormsQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SentenceFormsQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to SentenceFormsQueryRepository here
    }
}
