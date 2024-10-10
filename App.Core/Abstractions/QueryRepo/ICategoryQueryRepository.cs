using App.Domain.Abstractions.QueryRepo.Base;
using App.Domain.Entities;

namespace App.Domain.Abstractions.QueryRepo
{
    public interface ICategoryQueryRepository : IQueryRepository<Category>
    {
        // Add specific Query methods here if needed
    }
}
