using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.SentenceStructureFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;


namespace App.Infrastructure.Services
{
    public class SentenceStructureServices : ISentenceStructureServices
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        public SentenceStructureServices(IUowRepo uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }
        public async Task<(bool Success, string id)> CreateAsync(CreateSentenceStructureCommand entity)
        {
            var newVerb = new SentenceStructure
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = entity.CreatedBy,
                BanglaSentence = entity.BanglaSentence.Trim(),
                EnglistSentence = entity.EnglistSentence.Trim(),
                SubCatagoryID = entity.SubCatagoryID.Trim(),
                isAssaindByforms=entity.isAssaindByforms,
                FormsId=entity.FormsId
            };
            newVerb.SetCreatedDate(DateTime.Now, entity?.CreatedBy);
            await _uowRepo.sentencesStructureCommandRepository.AddAsync(newVerb);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newVerb.Id);
        }

        public Task<bool> CreateFormXlsxAsync(IFormFile file)
        {
            var excelProcessor = new ExcelProcessor();
            var sentences = excelProcessor.ReadExcelData(file);
            var result = _uowRepo.sentencesStructureCommandRepository.CreateSentencesForXlsx(sentences);
            return result;
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new BadRequestException($"Sentence Structure with id = {id} not found");
            }
            var SentenceStructure = await _uowRepo.sentencesStructureQueryRepository.GetByIdAsync(id);
            if (SentenceStructure == null)
            {
                throw new NotFoundException($"Sentence Structure with id = {id} not found");
            }
            await _uowRepo.sentencesStructureCommandRepository.DeleteAsync(SentenceStructure);
            await _uowRepo.SaveAsync();
            return (Success: true, id: id);
        }

        public async Task<IEnumerable<SentenceStructureDTOs>> GetAllAsync()
        {
            var itemList = await _uowRepo.sentencesStructureQueryRepository.GetAllSqlAsync();
            var SentenceStructures = itemList.Select(emp => _mapper.Map<SentenceStructureDTOs>(emp));
            return SentenceStructures;
        }

        public async Task<IEnumerable<SentenceStructureDTOs>> GetAllFilterAsync(string subCatagoryID, string formsId, int? pageSize, int? pageNumber)
        {
            // Call repository method and pass validated parameters
            var itemList = await _uowRepo.sentencesStructureQueryRepository
                .GetAllFilterBySubCatagoryIdAndFormsIdAsync(subCatagoryID, formsId, pageSize ?? 10, pageNumber ?? 1);

            // Map the result to DTOs and return
            var SentenceStructures = itemList.Select(emp => _mapper.Map<SentenceStructureDTOs>(emp));
            return SentenceStructures;
        }
        public async Task<SentenceStructureDTOs> GetByIdAsync(string id)
        {
            var sentenceStructure = await _uowRepo.sentencesStructureQueryRepository.GetByIdAsync(id);
            if (sentenceStructure == null)
            {
                throw new NotFoundException($"Sentence Structure with id = {id} not found");
            }
            return _mapper.Map<SentenceStructureDTOs>(sentenceStructure);
        }

        public async Task<(bool Success, string id)> UpdateAsync(UpdateSentenceStructureCommand entity)
        {
            var sentencesStructure = await _uowRepo.sentencesStructureQueryRepository.GetByIdAsync(entity?.id);
            if (sentencesStructure == null)
            {
                throw new NotFoundException($"Sentences Structure with id = {entity?.id} not found");
            }
            // Update  properties
            sentencesStructure.SetUpdateDate(DateTime.Now, entity.UpdatedBy);
            sentencesStructure.BanglaSentence = entity.BanglaSentence.Trim();
            sentencesStructure.EnglistSentence = entity.EnglistSentence.Trim();
            sentencesStructure.SubCatagoryID = entity.SubCatagoryID.Trim();
            sentencesStructure.isAssaindByforms = entity.isAssaindByforms;
            sentencesStructure.FormsId= entity.FormsId;
           

            await _uowRepo.sentencesStructureCommandRepository.UpdateAsync(sentencesStructure);
            await _uowRepo.SaveAsync();
            return (Success: true, id: sentencesStructure.Id);
        }
    }
}
