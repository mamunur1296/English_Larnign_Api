

namespace App.Ui.Models
{
    public class SubCategoryFormMapping : BaseModel
    {
        public string SubCategoryId { get; set; }
        public string SentenceFormId { get; set; }
        // Navigation properties
        public SubCategory SubCategory { get; set; }
        public SentenceForms SentenceForm { get; set; }
    }
}
