using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class DashboirdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
