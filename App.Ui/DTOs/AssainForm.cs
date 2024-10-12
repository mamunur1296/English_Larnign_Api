using App.Ui.Models;

namespace App.Ui.DTOs
{
    public class AssainForm : BaseModel
    {
        public string SubCategoryId { get; set; }
        public List<string> SentenceFormId { get; set; }
    }
}
