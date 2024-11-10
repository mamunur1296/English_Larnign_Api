using App.Domain.Abstractions.CommandRepo;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;
using App.Domain.Entities;
namespace App.Infrastructure.Implementation.Command
{
    internal class DescriptionCommandRepository : CommandRepository<Description>, IDescriptionCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DescriptionCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to CategoryCommandRepository here
    }
}
