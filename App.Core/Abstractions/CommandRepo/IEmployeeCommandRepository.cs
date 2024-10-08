using App.Domain.Abstractions.CommandRepo.Base;
using App.Domain.Entities;

namespace App.Domain.Abstractions.CommandRepo
{
    public interface IEmployeeCommandRepository : ICommandRepository<Employee>
    {
        // Add specific command methods here if needed
    }
}
