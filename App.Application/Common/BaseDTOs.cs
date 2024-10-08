

namespace App.Application.Common
{
    public class BaseDTOs
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; private set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public void SetUpdateDate(DateTime updateDate, string updatedBy)
        {
            UpdateDate = updateDate;
            UpdatedBy = updatedBy;
        }
        public void SetCreatedDate(DateTime creationDate, string createdBy)
        {
            CreationDate = creationDate;
            CreatedBy = createdBy;
        }
    }
}
