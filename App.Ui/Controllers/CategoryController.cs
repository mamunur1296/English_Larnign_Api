using App.Ui.ApiSettings;
using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Ui.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IClientServices<Category> _clintServices;
        private readonly CategoryUrlSettings _apiUrls;
        public CategoryController(IClientServices<Category> clintServices, IOptions<CategoryUrlSettings> apiUrls)
        {
            _clintServices = clintServices;
            _apiUrls=apiUrls.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            var Category = await _clintServices.PostClientAsync(_apiUrls.Category, model);
            return Json(Category);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var Category = await _clintServices.GetAllClientsAsync(_apiUrls.Category);
            return Json(Category);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var Category = await _clintServices.GetClientByIdAsync($"{_apiUrls.Category}/{id}");
            return Json(Category);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, Category model)
        {
            var result = await _clintServices.UpdateClientAsync($"{_apiUrls.Category}/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"{_apiUrls.Category}/{id}");
            return Json(result);
        }
    }
}
