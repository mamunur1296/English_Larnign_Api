using App.Application.Common;


namespace App.Application.DTOs
{
    public class SearchBySubCategoryDTOs : BaseDTOs
    {
        public SubCategoryDTOs SubCategoryDTOs { get; set; }= new SubCategoryDTOs();
        public List<SentenceFormsDTOs> SentencesForms { get; set;} = new List<SentenceFormsDTOs>();

    }
   
}
