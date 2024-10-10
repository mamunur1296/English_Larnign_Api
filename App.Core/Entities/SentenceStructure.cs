using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class SentenceStructure : BaseEntity
    {
        public string BanglaSentence { get; set; }
        public string EnglistSentence { get; set; }
        public ICollection<SentenceFormStructureMapping> SentenceFormStructureMappings { get; set; }
    }
}
