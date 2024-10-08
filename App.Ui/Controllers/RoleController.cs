using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
