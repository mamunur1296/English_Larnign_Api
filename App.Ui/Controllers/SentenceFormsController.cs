using App.Ui.ApiSettings;
using App.Ui.DTOs;
using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Ui.Controllers
{
    public class SentenceFormsController : Controller
    {
        private readonly IClientServices<SentenceForms> _clintServices;
        private readonly IClientServices<AssainStructureDTOs> _assainStructureServices;
        private readonly SentenceFormsUrlSettings _apiUrls;
        public SentenceFormsController(IClientServices<SentenceForms> clintServices, IOptions<SentenceFormsUrlSettings> apiUrls, IClientServices<AssainStructureDTOs> assainStructureServices)
        {
            _clintServices = clintServices;
            _apiUrls = apiUrls.Value;
            _assainStructureServices = assainStructureServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SentenceForms model)
        {
            var SentenceForms = await _clintServices.PostClientAsync(_apiUrls.SentenceForms, model);
            return Json(SentenceForms);
        }
        [HttpPost]
        public async Task<IActionResult> AssainStructure(AssainStructureDTOs model)
        {
            var assainStructure = await _assainStructureServices.PostClientAsync(_apiUrls.AssainStructure, model);
            return Json(assainStructure);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var SentenceForms = await _clintServices.GetAllClientsAsync(_apiUrls.SentenceForms);
            return Json(SentenceForms);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var SentenceForms = await _clintServices.GetClientByIdAsync($"{_apiUrls.SentenceForms}/{id}");
            return Json(SentenceForms);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, SentenceForms model)
        {
            var result = await _clintServices.UpdateClientAsync($"{_apiUrls.SentenceForms}/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"{_apiUrls.SentenceForms}/{id}");
            return Json(result);
        }
    }
}
