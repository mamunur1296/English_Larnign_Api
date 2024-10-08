using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IClientServices<Menu> _clintServices;

        public MenuController(IClientServices<Menu> clintServices)
        {
            _clintServices = clintServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Menu model)
        {
            var register = await _clintServices.PostClientAsync("Menu", model);
            return Json(register);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var roles = await _clintServices.GetAllClientsAsync("Menu");
            return Json(roles);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var menu = await _clintServices.GetClientByIdAsync($"Menu/{id}");
            return Json(menu);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, Menu model)
        {
            var result = await _clintServices.UpdateClientAsync($"Menu/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"Menu/{id}");
            return Json(result);
        }
    }
}
