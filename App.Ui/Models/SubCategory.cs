namespace App.Ui.Models
{
    public class SubCategory : BaseModel
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public ICollection<SentenceForms> SentenceForms { get; set; } = new List<SentenceForms>();
    }
}
