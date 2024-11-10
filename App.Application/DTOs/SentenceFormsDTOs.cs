using App.Application.Common;

namespace App.Application.DTOs
{
    public class SentenceFormsDTOs : BaseDTOs
    {
        public string Name { get; set; }
        public string? SubCategoryId { get; set; }
        public bool? isAssaindBySubCatagory { get; set; } = false;
        public string? body { get; set; }
        public List<SentenceStructureDTOs> SentenceStructures { get; set; } = new List<SentenceStructureDTOs>(); 
    }
}
