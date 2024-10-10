namespace App.Ui.Models
{
    public class SentenceForms : BaseModel
    {
        public string Name { get; set; }
        // Foreign key to SubCategory
        public virtual ICollection<SentenceFormStructureMapping>? SentenceFormStructureMapping { get; set; }
    }
}
