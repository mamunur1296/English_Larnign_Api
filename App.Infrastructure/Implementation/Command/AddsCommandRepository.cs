using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;

namespace App.Infrastructure.Implementation.Command
{
    public class AddsCommandRepository : CommandRepository<Adds>, IAddsCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AddsCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to CategoryCommandRepository here
    }
}
