using App.Domain.Entities.Base;


namespace App.Domain.Entities
{
    public class Description : BaseEntity
    {
        public string body { get; set; }
        public string bodyBangla { get; set; }
        public string formateId { get; set; }
        public string subCatagoryId { get; set; }
    }
}
