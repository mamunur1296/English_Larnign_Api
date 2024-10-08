using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
