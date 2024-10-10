
using App.Domain.Abstractions.CommandRepo.Base;
using App.Domain.Entities;

namespace App.Domain.Abstractions.CommandRepo
{
    public interface ICategoryCommandRepository : ICommandRepository<Category>
    {
        // Add specific command methods here if needed
    }
}
