

namespace App.Ui.Models
{
    public class SentenceFormStructureMapping : BaseModel
    {
        public string FormateID { get; set; }
        public string StructureID { get; set; }
        public SentenceForms SentenceForm { get; set; }
        public SentenceStructure SentenceStructure { get; set; }
    }
}
