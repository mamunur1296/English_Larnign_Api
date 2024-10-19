using App.Domain.Abstractions.CommandRepo.Base;
using App.Domain.Entities;
using App.Domain.OthersDto;

namespace App.Domain.Abstractions.CommandRepo
{
    public interface ISentenceStructureCommandRepository : ICommandRepository<SentenceStructure>
    {
        Task<bool> CreateSentencesForXlsx(List<InputSentenceItem> sentenceStructure);
    }
}
