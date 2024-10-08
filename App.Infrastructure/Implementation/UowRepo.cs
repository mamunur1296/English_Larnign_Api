using App.Domain.Abstractions;
using App.Domain.Abstractions.CommandRepo;
using App.Domain.Abstractions.QueryRepo;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command;
using App.Infrastructure.Implementation.Query;

namespace App.Infrastructure.Implementation
{
    public class UowRepo : IUowRepo
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public IEmployeeCommandRepository employeeCommandRepository {  get; private set; }  

        public IEmployeeQueryRepository employeeQueryRepository { get; private set; }
        public UowRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            employeeCommandRepository = new EmployeeCommandRepository(applicationDbContext);
            employeeQueryRepository= new EmployeeQueryRepository(applicationDbContext);
        }

        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
