using App.Domain.Abstractions.CommandRepo;
using App.Domain.Abstractions.QueryRepo;


namespace App.Domain.Abstractions
{
    public interface IUowRepo
    {
        IEmployeeCommandRepository employeeCommandRepository { get; }
        IEmployeeQueryRepository employeeQueryRepository { get; }
        Task SaveAsync();
    }
}
