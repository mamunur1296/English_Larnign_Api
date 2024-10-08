using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.EmployeeFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infrastructure.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        public EmployeeServices(IUowRepo uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }

        public async Task<(bool Success, string id)> CreateAsync(CreateEmployeeCommand entity)
        {
            var newEmployee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                Address = entity.Address,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Phone = entity.Phone,
            };
            newEmployee.SetCreatedDate(DateTime.Now, entity?.CreatedBy);
            await _uowRepo.employeeCommandRepository.AddSqlAsync(newEmployee);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newEmployee.Id);
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new BadRequestException($"Employee with id = {id} not found");
            }
            var deleteEmployee = await _uowRepo.employeeQueryRepository.GetByIdSqlAsync(id);
            if (deleteEmployee == null)
            {
                throw new NotFoundException($"Employee with id = {id} not found");
            }
            await _uowRepo.employeeCommandRepository.DeleteSqlAsync(id);
            await _uowRepo.SaveAsync();
            return (Success: true, id: id);
        }

        public async Task<IEnumerable<EmployeeDTOs>> GetAllAsync()
        {
            var employeeList = await _uowRepo.employeeQueryRepository.GetAllSqlAsync();
            var employees = employeeList.Select(emp => _mapper.Map<EmployeeDTOs>(emp));
            return employees;
        }

        public async Task<EmployeeDTOs> GetByIdAsync(string id)
        {
            var employee = await _uowRepo.employeeQueryRepository.GetByIdSqlAsync(id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with id = {id} not found");
            }
            return  _mapper.Map<EmployeeDTOs>(employee);
        }

        public async Task<(bool Success, string id)> UpdateAsync( UpdateEmployeeCommand entity)
        {
            var employee = await _uowRepo.employeeQueryRepository.GetByIdAsync(entity?.id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with id = {entity?.id} not found");
            }
            // Update  properties

            employee.SetUpdateDate(DateTime.Now, entity.UpdatedBy);
            employee.FirstName = entity.FirstName;
            employee.LastName = entity.LastName;
            employee.Address = entity.Address;
            employee.Phone = entity.Phone;

            await _uowRepo.employeeCommandRepository.UpdateAsync(employee);
            await _uowRepo.SaveAsync();
            return (Success: true, id: employee.Id);
        }
    }
}
