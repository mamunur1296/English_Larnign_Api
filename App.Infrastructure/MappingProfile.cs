using App.Application.DTOs;
using App.Domain.Entities;
using App.Infrastructure.Identity;
using AutoMapper;


namespace App.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Employee, EmployeeDTOs>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
