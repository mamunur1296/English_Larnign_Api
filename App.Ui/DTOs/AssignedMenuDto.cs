namespace App.Ui.DTOs
{
    public class AssignedMenuDto
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public List<AssignedSubMenuDto> SubMenus { get; set; }
    }

    public class AssignedSubMenuDto
    {
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuUrl { get; set; }
        public List<AssignedActionDto> Actions { get; set; }
    }

    public class AssignedActionDto
    {
        public string ActionId { get; set; }
        public string ActionName { get; set; }
    }
}
