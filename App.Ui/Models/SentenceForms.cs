namespace App.Ui.Models
{
    public class SentenceForms : BaseModel
    {
        public string Name { get; set; }
        public bool? isAssaindBySubCatagory { get; set; } = false;
        public List<SentenceStructure> SentenceStructures { get; set; } = new List<SentenceStructure>();
    }
}
