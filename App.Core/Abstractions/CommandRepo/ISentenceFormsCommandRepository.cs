using App.Domain.Abstractions.CommandRepo.Base;
using App.Domain.Entities;

namespace App.Domain.Abstractions.CommandRepo
{
    public interface ISentenceFormsCommandRepository : ICommandRepository<SentenceForms>
    {
        // Add specific command methods here if needed
        Task<bool> CreateSentenceStructureAndFormateMapping(string formateID, List<string> structureIDs);
        Task<bool> UpdateSentenceStructureAndFormateMapping(string formateID, List<string> structureIDs);
    }
    
}
