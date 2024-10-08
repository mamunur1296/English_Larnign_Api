using App.Ui.DTOs;

namespace App.Ui.Models
{
    public class SubMenu
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string? Url { get; set; }

        public string MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<ActionNameDTO>? SubMenuActions { get; set; }
    }
}
