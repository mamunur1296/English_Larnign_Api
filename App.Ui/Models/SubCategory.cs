namespace App.Ui.Models
{
    public class SubCategory : BaseModel
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public virtual ICollection<SubCategoryFormMapping>? SubCategoryFormMapping { get; set; } = new List<SubCategoryFormMapping>();
    }
}
