namespace App.Ui.DTOs
{
    public class PostMapingDTO
    {
        public string RoleId { get; set; }
        public List<MenuSelection> Menus { get; set; } = new List<MenuSelection>();
    }
    public class MenuSelection
    {
        public string MenuId { get; set; }
        public List<SubMenuSelection> SubMenus { get; set; } = new List<SubMenuSelection>();
    }
    public class SubMenuSelection
    {
        public string SubMenuId { get; set; }
        public List<ActionSelection> Actions { get; set; } = new List<ActionSelection>();
    }
    public class ActionSelection
    {
        public string ActionId { get; set; }
    }
}
