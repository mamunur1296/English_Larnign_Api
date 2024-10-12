namespace App.Ui.Models
{
    public class SentenceStructure : BaseModel
    {
        public string BanglaSentence { get; set; }
        public string EnglistSentence { get; set; }
        public string SubCatagoryID { get; set; }
        public ICollection<SentenceFormStructureMapping> SentenceFormStructureMappings { get; set; }
    }
}
