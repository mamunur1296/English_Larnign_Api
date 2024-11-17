using App.Ui.ApiSettings;
using App.Ui.DTOs;
using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Ui.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public class SubCategoryController : Controller
    {
        private readonly IClientServices<SubCategory> _clintServices;
        private readonly IClientServices<AssainForm> _assainFormServices;
        private readonly SubCategoryUrlSettings _apiUrls;
        public SubCategoryController(IClientServices<SubCategory> clintServices, IOptions<SubCategoryUrlSettings> apiUrls, IClientServices<AssainForm> assainFormServices)
        {
            _clintServices = clintServices;
            _apiUrls = apiUrls.Value;
            _assainFormServices = assainFormServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssainForm(AssainForm model)
        {
            var AssainForm = await _assainFormServices.PostClientAsync(_apiUrls.AssainForms, model);
            return Json(AssainForm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubCategory model)
        {
            var SubCategory = await _clintServices.PostClientAsync(_apiUrls.SubCategory, model);
            return Json(SubCategory);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var SubCategory = await _clintServices.GetAllClientsAsync(_apiUrls.SubCategory);
            return Json(SubCategory);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var SubCategory = await _clintServices.GetClientByIdAsync($"{_apiUrls.SubCategory}/{id}");
            return Json(SubCategory);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, SubCategory model)
        {
            var result = await _clintServices.UpdateClientAsync($"{_apiUrls.SubCategory}/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"{_apiUrls.SubCategory}/{id}");
            return Json(result);
        }
    }
}
