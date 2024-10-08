using App.Application.DTOs;
using App.Application.Features.EmployeeFeatures.CommandHandlers;


namespace App.Application.Interfaces
{
    public interface IEmployeeServices
    {
        Task<( bool Success,string id)> CreateAsync(CreateEmployeeCommand entity);
        Task<IEnumerable<EmployeeDTOs>> GetAllAsync();
        Task<EmployeeDTOs> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateEmployeeCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
    }
}
