using App.Ui.DTOs;
using App.Ui.Services.Interface;
using App.Ui.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Ui.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IClientServices<MenuDTO> _clientServices;
        private readonly IClientServices<PostMapingDTO> _Services;

        public AuthorizationController(IClientServices<MenuDTO> clientServices, IClientServices<PostMapingDTO> services)
        {
            _clientServices = clientServices;
            _Services = services;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new AuthorizationVm();
            var menus = await _clientServices.GetAllClientsAsync("Menu");
            if (menus?.Data != null)
            {
                vm.Menus = menus?.Data.ToList();
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult GetUserMenus()
        {
            var userMenus = HttpContext.Session.GetString("UserMenus");
            if (string.IsNullOrEmpty(userMenus))
            {
                return Json(new { success = false, message = "No menus found" });
            }

            return Json(new { success = true, data = userMenus });
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var menus = await _clientServices.GetAllClientsAsync("Menu");
            return Json(menus);
        }
        [HttpPost]
        public async Task<IActionResult> SaveRoleMenuMapping([FromForm] AuthorizationVm model)
        {
            // Initialize PostMappingDTO
            var postMappingData = new PostMapingDTO
            {
                RoleId = model.RoleId,
                Menus = new List<MenuSelection>()
            };

            // Loop through Menus
            foreach (var menu in model.Menus)
            {
                if (menu.IsSelected)
                {
                    var menuSelection = new MenuSelection
                    {
                        MenuId = menu.Id,
                        SubMenus = new List<SubMenuSelection>()
                    };

                    // Loop through SubMenus
                    if (menu.SubMenus != null)
                    {
                        foreach (var subMenu in menu.SubMenus)
                        {
                            if (subMenu.IsSelected)
                            {
                                var subMenuSelection = new SubMenuSelection
                                {
                                    SubMenuId = subMenu.Id,
                                    Actions = new List<ActionSelection>()
                                };

                                // Loop through Actions in SubMenus
                                if (subMenu.SubMenuActions != null)
                                {
                                    foreach (var action in subMenu.SubMenuActions)
                                    {
                                        if (action.IsSelected)
                                        {
                                            var actionSelection = new ActionSelection
                                            {
                                                ActionId = action.Id
                                            };
                                            subMenuSelection.Actions.Add(actionSelection);
                                        }
                                    }
                                }

                                // Add SubMenu to MenuSelection
                                menuSelection.SubMenus.Add(subMenuSelection);
                            }
                        }
                    }

                    // Add Menu to PostMapingDTO
                    postMappingData.Menus.Add(menuSelection);
                }
            }

            // Post the data to the API asynchronously and await the result
            var result = await _Services.PostClientAsync("Authorization/UpdateAuthorization", postMappingData);

            return Json(result);

            
        }


    }
}
