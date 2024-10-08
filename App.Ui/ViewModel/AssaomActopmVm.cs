using App.Ui.DTOs;
using App.Ui.Models;

namespace App.Ui.ViewModel
{
    public class AssaomActopmVm
    {
        public AssignActionsDTO AssignActionsDTO { get; set; } = new AssignActionsDTO();
        public IEnumerable<ActionNameDTO> actions { get; set; } = new List<ActionNameDTO>();
        public SubMenu SubMenu { get; set; } = new SubMenu(); 
    }
}
