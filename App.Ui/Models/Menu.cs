namespace App.Ui.Models
{
    public class Menu
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string? Url { get; set; }
        public virtual ICollection<SubMenu>? SubMenus { get; set; }
    }
}
