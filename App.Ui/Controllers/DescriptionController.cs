using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class DescriptionController : Controller
    {
        private readonly IClientServices<Description> _clintServices;

        public DescriptionController(IClientServices<Description> clintServices)
        {
            _clintServices = clintServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Description model)
        {
            var register = await _clintServices.PostClientAsync("Description", model);
            return Json(register);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var roles = await _clintServices.GetAllClientsAsync("Description");
            return Json(roles);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var menu = await _clintServices.GetClientByIdAsync($"Description/{id}");
            return Json(menu);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, Description model)
        {
            var result = await _clintServices.UpdateClientAsync($"Description/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"Description/{id}");
            return Json(result);
        }
    }
}
