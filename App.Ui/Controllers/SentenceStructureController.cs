using App.Ui.ApiSettings;
using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Ui.Controllers
{
    public class SentenceStructureController : Controller
    {
        private readonly IClientServices<SentenceStructure> _clintServices;
        private readonly SentenceStructureUrlSettings _apiUrls;
        public SentenceStructureController(IClientServices<SentenceStructure> clintServices, IOptions<SentenceStructureUrlSettings> apiUrls)
        {
            _clintServices = clintServices;
            _apiUrls = apiUrls.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SentenceStructure model)
        {
            var SentenceStructure = await _clintServices.PostClientAsync(_apiUrls.SentenceStructure, model);
            return Json(SentenceStructure);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var SentenceStructure = await _clintServices.GetAllClientsAsync(_apiUrls.SentenceStructure);
            return Json(SentenceStructure);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllByExistingFormId(string id)
        {
            var SentenceStructure = await _clintServices.GetAllClientsAsync(_apiUrls.SentenceStructure);
            if (SentenceStructure.Success)
            {
                var ExistingSentenceStructure = SentenceStructure?.Data?.Where(sa=>sa.FormsId==id && sa.isAssaindByforms==false);
                return Json(ExistingSentenceStructure);
            }
            return Json(SentenceStructure);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var SentenceStructure = await _clintServices.GetClientByIdAsync($"{_apiUrls.SentenceStructure}/{id}");
            return Json(SentenceStructure);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, SentenceStructure model)
        {
            var result = await _clintServices.UpdateClientAsync($"{_apiUrls.SentenceStructure}/{id}", model);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clintServices.DeleteClientAsync($"{_apiUrls.SentenceStructure}/{id}");
            return Json(result);
        }
    }
}
