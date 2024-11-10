using App.Domain.Abstractions.QueryRepo;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;
using App.Domain.Entities;

namespace App.Infrastructure.Implementation.Query
{
    public class DescriptionQueryRepository : QueryRepository<Description>, IDescriptionQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DescriptionQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to CategoryQueryRepository here
    }
}
