using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IClientServices<Category> _clintServices;

        public CategoryController(IClientServices<Category> clintServices)
        {
            _clintServices = clintServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            var Category = await _clintServices.PostClientAsync("Category", model);
            return Json(Category);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var Category = await _clintServices.GetAllClientsAsync("Category");
            return Json(Category);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var Category = await _clintServices.GetClientByIdAsync($"Category/{id}");
            return Json(Category);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, Category model)
        {
            var result = await _clintServices.UpdateClientAsync($"Category/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"Category/{id}");
            return Json(result);
        }
    }
}
