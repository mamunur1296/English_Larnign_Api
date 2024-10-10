using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.CategoryFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infrastructure.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        public CategoryServices(IUowRepo uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }
        public async Task<(bool Success, string id)> CreateAsync(CreateCategoryCommand entity)
        {
            var newCategory = new Category
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = entity.CreatedBy,
                Name = entity.Name.Trim(),

            };
            newCategory.SetCreatedDate(DateTime.Now, entity?.CreatedBy);
            await _uowRepo.categoryCommandRepository.AddSqlAsync(newCategory);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newCategory.Id);
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new BadRequestException($"Category with id = {id} not found");
            }
            var deleteitem = await _uowRepo.categoryQueryRepository.GetByIdSqlAsync(id);
            if (deleteitem == null)
            {
                throw new NotFoundException($"Category with id = {id} not found");
            }
            await _uowRepo.categoryCommandRepository.DeleteSqlAsync(id);
            await _uowRepo.SaveAsync();
            return (Success: true, id: id);
        }

        public async Task<IEnumerable<CategoryDTOs>> GetAllAsync()
        {
            var itemList = await _uowRepo.categoryQueryRepository.GetAllSqlAsync();
            var Category = itemList.Select(emp => _mapper.Map<CategoryDTOs>(emp));
            return Category;
        }

        public async Task<CategoryDTOs> GetByIdAsync(string id)
        {
            var Category = await _uowRepo.categoryQueryRepository.GetByIdSqlAsync(id);
            if (Category == null)
            {
                throw new NotFoundException($"Category with id = {id} not found");
            }
            return _mapper.Map<CategoryDTOs>(Category);
        }

        public async Task<(bool Success, string id)> UpdateAsync(UpdateCategoryCommand entity)
        {
            var category = await _uowRepo.categoryQueryRepository.GetByIdAsync(entity?.id);
            if (category == null)
            {
                throw new NotFoundException($"category with id = {entity?.id} not found");
            }
            // Update  properties
            category.SetUpdateDate(DateTime.Now, entity.UpdatedBy);
            category.Name = entity.Name.Trim();
           

            await _uowRepo.categoryCommandRepository.UpdateAsync(category);
            await _uowRepo.SaveAsync();
            return (Success: true, id: category.Id);
        }
    }
}
