using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.DescriptionFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
namespace App.Infrastructure.Services
{
    public class DescriptionServices : IDescriptionServices
    {
        private readonly IUowRepo _uowRepo;

        public DescriptionServices(IUowRepo uowRepo)
        {
            _uowRepo = uowRepo;
        }

        public async Task<(bool Success, string id)> CreateAsync(CreateDescriptionCommand entity)
        {
            var newDescription = new Description
            {
                Id = Guid.NewGuid().ToString(),
                subCatagoryId = entity.subCatagoryId,
                formateId = entity.formateId,
                body=entity.body,

            };
            await _uowRepo.descriptionCommandRepository.AddSqlAsync(newDescription);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newDescription.Id);
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new BadRequestException($"Description with id = {id} not found");
            }
            var deleteitem = await _uowRepo.descriptionQueryRepository.GetByIdSqlAsync(id);
            if (deleteitem == null)
            {
                throw new NotFoundException($"Description with id = {id} not found");
            }
            await _uowRepo.descriptionCommandRepository.DeleteSqlAsync(id);
            await _uowRepo.SaveAsync();
            return (Success: true, id: id);
        }

        public async Task<IEnumerable<DescriptionDTOs>> GetAllAsync()
        {
            var itemList = await _uowRepo.descriptionQueryRepository.GetAllSqlAsync();
            var result = itemList.Select(des => new DescriptionDTOs()
            {
                Id= des.Id,
                body= des.body,
                formateId=des.formateId,
                subCatagoryId=des.subCatagoryId
            });
            return result;
        }

        public async Task<DescriptionDTOs> GetByIdAsync(string id)
        {
            var Description = await _uowRepo.descriptionQueryRepository.GetByIdSqlAsync(id);
            if (Description == null)
            {
                throw new NotFoundException($"Description with id = {id} not found");
            }
            var result = new DescriptionDTOs() {
                Id = Description.Id,
                body = Description.body,
                formateId = Description.formateId,
                subCatagoryId = Description.subCatagoryId
            };
            return result;
        }

        public async Task<(bool Success, string id)> UpdateAsync(UpdateDescriptionCommand entity)
        {
            var description = await _uowRepo.descriptionQueryRepository.GetByIdAsync(entity?.id);
            if (description == null)
            {
                throw new NotFoundException($"Description with id = {entity?.id} not found");
            }

            description.body= entity.body;
            description.subCatagoryId= entity.subCatagoryId;
            description.formateId= entity.formateId;


            await _uowRepo.descriptionCommandRepository.UpdateAsync(description);
            await _uowRepo.SaveAsync();
            return (Success: true, id: description.Id);
        }
    }
}
