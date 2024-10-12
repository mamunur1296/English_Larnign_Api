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
            CreateMap<Verb, VerbDTOs>().ReverseMap();
            CreateMap<SentenceStructure, SentenceStructureDTOs>().ReverseMap();
            CreateMap<SentenceForms, SentenceFormsDTOs>()
             .ForMember(dest => dest.SentenceStructures, opt => opt.MapFrom(src => src.SentenceFormStructureMapping.Select(scfm => scfm.SentenceStructure)));
            CreateMap<SubCategory, SubCategoryDTOs>()
            .ForMember(dest => dest.SentenceForms, opt => opt.MapFrom(src => src.SubCategoryFormMapping.Select(scfm => scfm.SentenceForm)));
            CreateMap<Category, CategoryDTOs>().ReverseMap();
        }
    }
}
