using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.VerbFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;


namespace App.Infrastructure.Services
{
    public class VerbServices : IVerbServices
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        public VerbServices(IUowRepo uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }
        public async Task<(bool Success, string id)> CreateAsync(CreateVerbCommand entity)
        {
            var newVerb = new Verb
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = entity.CreatedBy,
                Name = entity.Name.Trim(),
                BanglaName = entity.BanglaName.Trim(),
                BaseForm = entity.BaseForm.Trim(),
                ThirdPersonSingular = entity.ThirdPersonSingular.Trim(),
                PastSimple = entity.PastSimple.Trim(),
                PastParticiple = entity.PastParticiple.Trim(),
                PresentParticiple = entity.PresentParticiple.Trim(),
                Gerund = entity.Gerund.Trim()


            };
            newVerb.SetCreatedDate(DateTime.Now, entity?.CreatedBy);
            await _uowRepo.verbCommandRepository.AddSqlAsync(newVerb);
            await _uowRepo.SaveAsync();
            return (Success: true, id: newVerb.Id);
        }

        public async Task<(bool Success, string id)> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new BadRequestException($"Verb with id = {id} not found");
            }
            var deleteitem = await _uowRepo.verbQueryRepository.GetByIdSqlAsync(id);
            if (deleteitem == null)
            {
                throw new NotFoundException($"Verb with id = {id} not found");
            }
            await _uowRepo.verbCommandRepository.DeleteSqlAsync(id);
            await _uowRepo.SaveAsync();
            return (Success: true, id: id);
        }

        public async Task<IEnumerable<VerbDTOs>> GetAllAsync()
        {
            var itemList = await _uowRepo.verbQueryRepository.GetAllSqlAsync();
            var verbs = itemList.Select(emp => _mapper.Map<VerbDTOs>(emp));
            return verbs;
        }

        public async Task<VerbDTOs> GetByIdAsync(string id)
        {
            var verb = await _uowRepo.verbQueryRepository.GetByIdSqlAsync(id);
            if (verb == null)
            {
                throw new NotFoundException($"Verb with id = {id} not found");
            }
            return _mapper.Map<VerbDTOs>(verb);
        }

        public async Task<(bool Success, string id)> UpdateAsync(UpdateVerbCommand entity)
        {
            var verb = await _uowRepo.verbQueryRepository.GetByIdAsync(entity?.id);
            if (verb == null)
            {
                throw new NotFoundException($"verb with id = {entity?.id} not found");
            }
            // Update  properties
            verb.SetUpdateDate(DateTime.Now, entity.UpdatedBy);
            verb.Name=entity.Name.Trim();
            verb.BanglaName = entity.BanglaName.Trim();
            verb.BaseForm=entity.BaseForm.Trim();
            verb.ThirdPersonSingular=entity.ThirdPersonSingular.Trim();
            verb.PastSimple=entity.PastSimple.Trim();
            verb.PastParticiple=entity.PastParticiple.Trim();
            verb.PresentParticiple = entity.PresentParticiple.Trim();
            verb.Gerund = entity.Gerund.Trim();

            await _uowRepo.verbCommandRepository.UpdateAsync(verb);
            await _uowRepo.SaveAsync();
            return (Success: true, id: verb.Id);
        }
    }
}
