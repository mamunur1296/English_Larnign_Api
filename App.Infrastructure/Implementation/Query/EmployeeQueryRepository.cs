using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;


namespace App.Infrastructure.Implementation.Query
{
    public class EmployeeQueryRepository : QueryRepository<Employee>, IEmployeeQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to CompanyQueryRepository here
    }
}
