using App.Ui.Models;

namespace App.Ui.DTOs
{
    public class AssainStructureDTOs : BaseModel
    {
        public string formateID { get; set; }
        public List<string> structureIDs { get; set; }
    }
}
