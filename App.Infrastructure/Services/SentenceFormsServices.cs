using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.SentenceFormsFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infrastructure.Services
{
    public class SentenceFormsServices : ISentenceFormsServices
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        public SentenceFormsServices(IUowRepo uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }

        public async Task<bool> AssainStructure(string formateID, List<string> structureIDs)
        {
            var result = await _uowRepo.sentenceFormsCommandRepository.CreateSentenceStructureAndFormateMapping(formateID, structureIDs);
            if (!result)
            {
                throw new BadRequestException("Assain Structure Failed");
            }
            return true;
        }

        public async Task<(bool Success, string id)> CreateAsync(CreateSentenceFormsCommand entity)
        {
            var newSentenceForms = new SentenceForms
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = entity.CreatedBy,
                Name = entity.Name.Trim(),
                
            };
            newSentenceForms.SetCreatedDate(DateTime.Now, entity?.CreatedBy);
            await _uowRepo.sentenceFormsCommandRepository.AddAsync(newSentenceForms);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newSentenceForms.Id);
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new BadRequestException($"Sentence Forms with id = {id} not found");
            }
            var deleteitem = await _uowRepo.sentenceFormsQueryRepository.GetByIdSqlAsync(id);
            if (deleteitem == null)
            {
                throw new NotFoundException($"Sentence Forms with id = {id} not found");
            }
            await _uowRepo.sentenceFormsCommandRepository.DeleteSqlAsync(id);
            await _uowRepo.SaveAsync();
            return (Success: true, id: id);
        }

        public async Task<IEnumerable<SentenceFormsDTOs>> GetAllAsync()
        {
            var itemList = await _uowRepo.sentenceFormsQueryRepository.GetAllSentenceFormsWithStructure();
            var SentenceForms = itemList.Select(emp => _mapper.Map<SentenceFormsDTOs>(emp));
            return SentenceForms;
        }

        public async Task<SentenceFormsDTOs> GetByIdAsync(string id)
        {
            var verb = await _uowRepo.sentenceFormsQueryRepository.GetByIdSqlAsync(id);
            if (verb == null)
            {
                throw new NotFoundException($"Sentence Forms with id = {id} not found");
            }
            return _mapper.Map<SentenceFormsDTOs>(verb);
        }

        public async Task<(bool Success, string id)> UpdateAsync(UpdateSentenceFormsCommand entity)
        {
            var verb = await _uowRepo.sentenceFormsQueryRepository.GetByIdAsync(entity?.id);
            if (verb == null)
            {
                throw new NotFoundException($"Sentence Forms with id = {entity?.id} not found");
            }
            // Update  properties
            verb.SetUpdateDate(DateTime.Now, entity.UpdatedBy);
            verb.Name = entity.Name.Trim();
           
           
            await _uowRepo.sentenceFormsCommandRepository.UpdateAsync(verb);
            await _uowRepo.SaveAsync();
            return (Success: true, id: verb.Id);
        }
    }
}
