using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;


namespace App.Infrastructure.Implementation.Query
{
    public class SentenceStructureQueryRepository : QueryRepository<SentenceStructure>, ISentenceStructureQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SentenceStructureQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to SentenceStructureQueryRepository here
    }
}
