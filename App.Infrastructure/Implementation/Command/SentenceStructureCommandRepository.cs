using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;


namespace App.Infrastructure.Implementation.Command
{
    public class SentenceStructureCommandRepository : CommandRepository<SentenceStructure>, ISentenceStructureCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SentenceStructureCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to SentenceStructureCommandRepository here
    }
}
