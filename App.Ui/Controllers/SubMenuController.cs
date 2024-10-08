using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using App.Ui.Models;
using App.Ui.DTOs;
using App.Ui.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace App.Ui.Controllers
{

    [Authorize]
    public class SubMenuController : Controller
    {
        private readonly IClientServices<SubMenu> _clintServices;
        private readonly IClientServices<AssignActionsDTO> _assainActionServices;
        private readonly IClientServices<ActionNameDTO> _actionsServices;
        public SubMenuController(IClientServices<SubMenu> clintServices, IClientServices<AssignActionsDTO> assainActionServices, IClientServices<ActionNameDTO> actionsServices)
        {
            _clintServices = clintServices;
            _assainActionServices = assainActionServices;
            _actionsServices = actionsServices;
        }
        public async Task<IActionResult> Index()
        {
            var actions = await _actionsServices.GetAllClientsAsync("ActionName");
            var vm = new AssaomActopmVm();
            if (actions.Success)
            {
                vm.actions = actions?.Data;
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AssaomActopmVm model)
        {
            var register = await _clintServices.PostClientAsync("SubMenu", model.SubMenu);
            return Json(register);
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var roles = await _clintServices.GetAllClientsAsync("SubMenu");
            return Json(roles);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var subMenu = await _clintServices.GetClientByIdAsync($"SubMenu/{id}");
            return Json(subMenu);
        }
        [HttpGet]
        public async Task<IActionResult> GetallActions()
        {
            var actions = await _actionsServices.GetAllClientsAsync("ActionName");
            return Json(actions);
        }
        [HttpPost]
        public async Task<IActionResult> AssainActions([FromForm] AssaomActopmVm model)
        {
            var register = await _assainActionServices.PostClientAsync("SubMenu/AssignActions", model.AssignActionsDTO);
            return Json(register);
        }
    }
}
