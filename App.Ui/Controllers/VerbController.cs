using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace App.Ui.Controllers
{
    public class VerbController : Controller
    {
        private readonly IClientServices<Verb> _clintServices;

        public VerbController(IClientServices<Verb> clintServices)
        {
            _clintServices = clintServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Verb model)
        {
            var Verb = await _clintServices.PostClientAsync("Verb", model);
            return Json(Verb);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var Verb = await _clintServices.GetAllClientsAsync("Verb");
            return Json(Verb);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var Verb = await _clintServices.GetClientByIdAsync($"Verb/{id}");
            return Json(Verb);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, Verb model)
        {
            var result = await _clintServices.UpdateClientAsync($"Verb/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"Verb/{id}");
            return Json(result);
        }
    }
}
