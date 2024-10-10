using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class SentenceFormStructureMapping : BaseEntity
    {
        public string FormateID { get; set; } 
        public string StructureID { get; set; }
        public SentenceForms SentenceForm { get; set; }
        public SentenceStructure SentenceStructure { get; set; }
    }
}
