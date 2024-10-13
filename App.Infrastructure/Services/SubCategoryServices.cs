using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.SubCategoryFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infrastructure.Services
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        public SubCategoryServices(IUowRepo uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }

        public async Task<bool> AssainForms(string SubCategoryId, List<string> SentenceFormId)
        {
            var result = await _uowRepo.subCategoryCommandRepository.UpdateAssainFormMapping(SubCategoryId, SentenceFormId);
            return result;
        }

        public async Task<(bool Success, string id)> CreateAsync(CreateSubCategoryCommand entity)
        {
            var newSubCategory = new SubCategory
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = entity.CreatedBy,
                Name = entity.Name.Trim(),
                CategoryId = entity.CategoryId.Trim(),
            };
            newSubCategory.SetCreatedDate(DateTime.Now, entity?.CreatedBy);
            await _uowRepo.subCategoryCommandRepository.AddAsync(newSubCategory);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newSubCategory.Id);
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new BadRequestException($"Sub Category with id = {id} not found");
            }
            var deleteitem = await _uowRepo.subCategoryQueryRepository.GetByIdAsync(id);
            if (deleteitem == null)
            {
                throw new NotFoundException($" Sub Category with id = {id} not found");
            }
            await _uowRepo.subCategoryCommandRepository.DeleteAsync(deleteitem);
            await _uowRepo.SaveAsync();
            return (Success: true, id: id);
        }

        public async Task<IEnumerable<SubCategoryDTOs>> GetAllAsync()
        {
            var itemList = await _uowRepo.subCategoryQueryRepository.GetAllSubCategoryWithForms();
            var verbs = itemList.Select(emp => _mapper.Map<SubCategoryDTOs>(emp));
            return verbs;
        }

        public async Task<SubCategoryDTOs> GetByIdAsync(string id)
        {
            var SubCatagory = await _uowRepo.subCategoryQueryRepository.GetByIdAsync(id);
            if (SubCatagory == null)
            {
                throw new NotFoundException($"SubCatagory with id = {id} not found");
            }
            return _mapper.Map<SubCategoryDTOs>(SubCatagory);
        }

        public async Task<SearchBySubCategoryDTOs> SearchBySubCategory(string subCategoryId, string verbId)
        {
            // Step 1: Fetch the SubCategory using the custom query method in the repository
            var subCategory = await _uowRepo.subCategoryQueryRepository.GetSubCategoryWithSentenceFormsAndStructures(subCategoryId);
            if (subCategory == null)
            {
                throw new NotFoundException("SubCategory not found.");
            }

            // Step 2: Fetch the verb using its id to get verb details (assuming you have a repository for this)
            var verb = await _uowRepo.verbQueryRepository.GetByIdAsync(verbId);
            if (verb == null)
            {
                throw new NotFoundException("Verb not found.");
            }

            // Step 3: Map SubCategory to SubCategoryDTOs
            var subCategoryDTO = new SubCategoryDTOs
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId
            };

            // Step 4: Prepare the SentenceForms with modified sentences
            var sentenceFormsDTOs = subCategory.SubCategoryFormMapping.Select(scfm => new SentenceFormsDTOs
            {
                Id = scfm.SentenceForm.Id,
                Name = scfm.SentenceForm.Name,
                SentenceStructures = scfm.SentenceForm.SentenceFormStructureMapping
                    .Where(sfsm => sfsm.SentenceStructure.SubCatagoryID == subCategoryDTO.Id) // Filter based on SubCategory ID
                    .Select(sfsm => new SentenceStructureDTOs
                    {
                        Id = sfsm.SentenceStructure.Id,
                        SubCatagoryID = sfsm.SentenceStructure?.SubCatagoryID,
                        // Modify BanglaSentence by replacing placeholders with the verb's banglaName
                        BanglaSentence = ModifySentence(sfsm.SentenceStructure.BanglaSentence, verb),
                        // Modify EnglishSentence by replacing placeholders with the verb's forms
                        EnglistSentence = ModifySentence(sfsm.SentenceStructure.EnglistSentence, verb)
                    }).ToList()
                        }).ToList();


            // Step 5: Return the mapped data in the SearchBySubCategoryDTOs object
            var result = new SearchBySubCategoryDTOs
            {
                Id = subCategoryDTO.Id,
                SubCategoryDTOs = subCategoryDTO,
                SentencesForms = sentenceFormsDTOs
            };

            return result;
        }

        private string ModifySentence(string sentence, Verb verb)
        {
            if (string.IsNullOrEmpty(sentence))
                return sentence;

            // Replace placeholders with corresponding values from the verb object
            return sentence
                .Replace("[banglaName]", verb.BanglaName ?? string.Empty)
                .Replace("[baseForm]", verb.BaseForm ?? string.Empty)
                .Replace("[pastSimple]", verb.PastSimple ?? string.Empty)
                .Replace("[pastParticiple]", verb.PastParticiple ?? string.Empty)
                .Replace("[presentParticiple]", verb.PresentParticiple ?? string.Empty)
                .Replace("[thirdPersonSingular]", verb.ThirdPersonSingular ?? string.Empty)
                .Replace("[gerund]", verb.Gerund ?? string.Empty);
        }













        //public async Task<SearchBySubCategoryDTOs> SearchBySubCategory(string subCategoryId, string verbId)
        //{
        //    // Step 1: Fetch the SubCategory using the custom query method in the repository
        //    var subCategory = await _uowRepo.subCategoryQueryRepository.GetSubCategoryWithSentenceFormsAndStructures(subCategoryId);

        //    if (subCategory == null)
        //    {
        //        throw new NotFoundException("SubCategory not found.");
        //    }

        //    // Step 2: Map SubCategory to SubCategoryDTOs
        //    var subCategoryDTO = new SubCategoryDTOs
        //    {
        //        Id = subCategory.Id,
        //        Name = subCategory.Name,
        //        CategoryId = subCategory.CategoryId
        //    };
        //    var sentenceForms = subCategory.SubCategoryFormMapping.Select(scfm => scfm.SentenceForm).ToList();

        //    foreach (var form in sentenceForms)
        //    {
        //        var structureCount = form.SentenceFormStructureMapping.Count;
        //        Console.WriteLine($"Sentence Form: {form.Name}, Structure Count: {structureCount}");
        //    }

        //    var sentenceFormsDTOs = subCategory.SubCategoryFormMapping.Select(scfm => new SentenceFormsDTOs
        //    {
        //        Id = scfm.SentenceForm.Id,
        //        Name = scfm.SentenceForm.Name,
        //        SentenceStructures = scfm.SentenceForm.SentenceFormStructureMapping.Select(sfsm => new SentenceStructureDTOs
        //        {
        //            Id = sfsm.SentenceStructure.Id,
        //            BanglaSentence = sfsm.SentenceStructure.BanglaSentence,
        //            EnglistSentence = sfsm.SentenceStructure.EnglistSentence
        //        }).ToList()
        //    }).ToList();


        //    // Step 4: Return the mapped data in the SearchBySubCategoryDTOs object
        //    var result = new SearchBySubCategoryDTOs
        //    {
        //        Id = subCategoryDTO.Id, // Assuming BaseDTOs have an Id
        //        SubCategoryDTOs = subCategoryDTO,
        //        SentencesForms = sentenceFormsDTOs
        //    };

        //    return result;
        //}


        public async Task<(bool Success, string id)> UpdateAsync(UpdateSubCategoryCommand entity)
        {
            var subCategory = await _uowRepo.subCategoryQueryRepository.GetByIdAsync(entity?.id);
            if (subCategory == null)
            {
                throw new NotFoundException($"Sub Category with id = {entity?.id} not found");
            }
            // Update  properties
            subCategory.SetUpdateDate(DateTime.Now, entity.UpdatedBy);
            subCategory.Name = entity.Name.Trim();
            subCategory.CategoryId = entity.CategoryId.Trim();


            await _uowRepo.subCategoryCommandRepository.UpdateAsync(subCategory);
            await _uowRepo.SaveAsync();
            return (Success: true, id: subCategory.Id);
        }


    }
}
