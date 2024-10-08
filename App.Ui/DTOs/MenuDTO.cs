namespace App.Ui.DTOs
{
    public class MenuDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public string? Url { get; set; }
        public virtual List<SubMenuDTO>? SubMenus { get; set; }
    }
    public class SubMenuDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public string? Url { get; set; }
        public virtual List<ActionNameDTO>? SubMenuActions { get; set; }
    }
}
