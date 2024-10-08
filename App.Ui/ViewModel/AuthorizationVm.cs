using App.Ui.DTOs;

namespace App.Ui.ViewModel
{
    public class AuthorizationVm
    {
        public string RoleId { get; set; }
        public List<MenuDTO> Menus { get; set; } = new List<MenuDTO>();
    }
}
