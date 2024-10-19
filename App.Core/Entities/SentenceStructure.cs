using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class SentenceStructure : BaseEntity
    {
        public string ?BanglaSentence { get; set; }
        public string ?EnglistSentence { get; set; }
        public string ?SubCatagoryID { get; set; }   
        public string ? FormsId { get; set; }
        public bool ? isAssaindByforms { get; set; } = false;
        public ICollection<SentenceFormStructureMapping> SentenceFormStructureMappings { get; set; }
    }
}
