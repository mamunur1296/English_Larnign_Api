using App.Application.Common;


namespace App.Application.DTOs
{
    public class SubCategoryDTOs : BaseDTOs
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public ICollection<SentenceFormsDTOs> SentenceForms { get; set; }
    }
}
