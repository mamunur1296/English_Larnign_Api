using System.ComponentModel;

namespace App.Ui.Models
{
    public class Description : BaseModel
    {
        [DisplayName("Roles")]
        public string body { get; set; }
        public string bodyBangla { get; set; }
        [DisplayName("Format")]
        public string formateId { get; set; }
        [DisplayName("Tanse")]
        public string subCatagoryId { get; set; }
    }
}
