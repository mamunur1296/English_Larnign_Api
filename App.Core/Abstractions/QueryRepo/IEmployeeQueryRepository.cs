using App.Domain.Abstractions.QueryRepo.Base;
using App.Domain.Entities;


namespace App.Domain.Abstractions.QueryRepo
{
    public interface IEmployeeQueryRepository : IQueryRepository<Employee>
    {
        // Add specific Query methods here if needed
    }
}
