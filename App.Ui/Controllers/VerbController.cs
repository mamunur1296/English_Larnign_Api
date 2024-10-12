using App.Ui.ApiSettings;
using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Ui.Controllers
{
    public class VerbController : Controller
    {
        private readonly IClientServices<Verb> _clintServices;
        private readonly VerbUrlSettings _apiUrls;
        public VerbController(IClientServices<Verb> clintServices, IOptions<VerbUrlSettings> apiUrls)
        {
            _clintServices = clintServices;
            _apiUrls = apiUrls.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Verb model)
        {
            var Verb = await _clintServices.PostClientAsync(_apiUrls.Verb, model);
            return Json(Verb);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var Verb = await _clintServices.GetAllClientsAsync(_apiUrls.Verb);
            return Json(Verb);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var Verb = await _clintServices.GetClientByIdAsync($"{_apiUrls.Verb}/{id}");
            return Json(Verb);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, Verb model)
        {
            var result = await _clintServices.UpdateClientAsync($"{_apiUrls.Verb}/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"{_apiUrls.Verb}/{id}");
            return Json(result);
        }
    }
}
