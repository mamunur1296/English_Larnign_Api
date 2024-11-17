using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class AssainStructureController : Controller
    {
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
