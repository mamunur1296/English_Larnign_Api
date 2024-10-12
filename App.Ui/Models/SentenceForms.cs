namespace App.Ui.Models
{
    public class SentenceForms : BaseModel
    {
        public string Name { get; set; }
        // Foreign key to SubCategory
        public List<SentenceStructure> SentenceStructures { get; set; } = new List<SentenceStructure>();
    }
}
