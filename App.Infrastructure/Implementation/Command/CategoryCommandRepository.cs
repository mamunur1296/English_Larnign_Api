using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;

namespace App.Infrastructure.Implementation.Command
{
    public class CategoryCommandRepository : CommandRepository<Category>, ICategoryCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to CategoryCommandRepository here
    }
}
