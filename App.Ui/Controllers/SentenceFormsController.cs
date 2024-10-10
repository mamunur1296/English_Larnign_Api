using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class SentenceFormsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
