﻿using App.Ui.Models;
using App.Ui.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using App.Ui.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace App.Ui.Controllers
{
    public class AuthController : Controller
    {
        private readonly IClientServices<Register> _registerServices;
        private readonly IClientServices<Login> _loginServices;
        private readonly ITokenService _tokenService;
        private readonly ApiUrlSettings _apiUrls;
        private readonly IClientServices<AssignedMenuDto> _menuServices;

        public AuthController(IClientServices<Register> registerServices,
                              IClientServices<Login> loginServices,
                              ITokenService tokenService,
                              IOptions<ApiUrlSettings> apiUrls,
                              IClientServices<AssignedMenuDto> menuServices)
        {
            _registerServices = registerServices;
            _loginServices = loginServices;
            _tokenService = tokenService;
            _apiUrls = apiUrls.Value;
            _menuServices = menuServices;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(Register model)
        {
            var loginModel = new Login();
            loginModel.UserName = model.UserName;
            loginModel.Password = model.Password;
            model.Roles = new List<string> { "User" };
            var register = await _registerServices.PostClientAsync(_apiUrls.RegisterUrl, model);

            if (register.Success)
            {
                var loginResponse = await _loginServices.Login(_apiUrls.LoginUrl, loginModel);
                if (loginResponse?.Data?.token != null)
                {
                    _tokenService.SaveToken(loginResponse.Data.token);
                    await UserLogin(loginResponse.Data.token);
                }
                return RedirectToAction("Index", "Home");
            }

            // Check for specific error messages and add model state errors
            if (register.Detail.Contains("DuplicateUserName"))
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
            }
            else if (register.Detail.Contains("DuplicateEmail"))
            {
                ModelState.AddModelError("Email", "Email is already taken.");
            }
            else if (register.Detail.Contains("ConfirmationPassword"))
            {
                ModelState.AddModelError("ConfirmationPassword", "Password and confirmation password do not match.");
            }
            else
            {
                ModelState.AddModelError("", "Registration failed. Please try again.");
            }

            return View("Register", model);
        }
        [HttpGet]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(Login model, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = ReturnUrl;
                return View("Login", model);
            }

            var loginResponse = await _loginServices.Login("Auth/user_login", model);

            if (loginResponse?.Data?.token == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                ViewData["ReturnUrl"] = ReturnUrl;
                return View("Login", model);
            }

            // Save token to the token service
            _tokenService.SaveToken(loginResponse.Data.token);

            // Extract role from JWT token before UserLogin
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(loginResponse.Data.token);

            // Extract the roles from the token claims
            var roleClaims = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToList();
            string roleName = roleClaims.FirstOrDefault();

            // Call UserLogin to sign in the user
            await UserLogin(loginResponse.Data.token);

            // Fetch menus based on the role and store them in session
            var menuResult = await _menuServices.GetMenusByRoleName($"Authorization/GetAuthorizationsByRole/{roleName}");

            if (menuResult != null)
            {
                HttpContext.Session.SetString("UserMenus", JsonConvert.SerializeObject(menuResult.Data));
            }

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _tokenService.ClearToken();
            // Clear the session data, including the menu
            HttpContext.Session.Remove("UserMenus");
            return RedirectToAction("Index", "Home");
        }
        private async Task UserLogin(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var claim in jwt.Claims)
            {
                identity.AddClaim(claim);
            }

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Optionally extract and log user details
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userName = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var roles = jwt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }
    }
}