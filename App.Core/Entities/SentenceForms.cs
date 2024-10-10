using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class SentenceForms : BaseEntity
    {
        public string Name { get; set; }
        // Foreign key to SubCategory
        public virtual ICollection<SentenceFormStructureMapping>? SentenceFormStructureMapping { get; set; } 
    }
}
