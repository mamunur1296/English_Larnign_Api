using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;

namespace App.Infrastructure.Implementation.Command
{
    public class VerbCommandRepository : CommandRepository<Verb>, IVerbCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VerbCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to VerbCommandRepository here
    }
}
