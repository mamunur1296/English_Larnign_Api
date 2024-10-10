using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public virtual ICollection<SubCategoryFormMapping>? SubCategoryFormMapping { get; set; } = new List<SubCategoryFormMapping>();
    }
}
