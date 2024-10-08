using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.AuthFeatures.CommandHandlers;
using App.Application.Features.UserFeatures.CommandHandlers;
using App.Application.Interfaces;
using App.Domain.Abstractions;
using App.Infrastructure.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUowRepo _uowRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(IUowRepo uowRepo, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<(bool Success, string ErrorMessage)> ChangePassword(string OldPassword, string newPassword, string Userid)
        {
            var user = await _userManager.FindByIdAsync(Userid);
            if (user == null)
            {
                // Handle user not found
                throw new NotFoundException("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(user, OldPassword, newPassword);
            if (result.Succeeded)
            {
                // Password change successful
                return (true, null);
            }

            // Collect error messages
            var errors = result.Errors.Select(e => e.Description).ToList();
            var errorMessage = string.Join("; ", errors);

            return (false, errorMessage);
        }

        public async Task<(bool isSucceed, string userId)> CreateUserAsync(RegisterUserCommend model)
        {

            var user = new ApplicationUser()
            {
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                Email = model.Email.Trim(),
                PhoneNumber = model.Phone.Trim(),
                Longitude = model?.Longitude?.Trim(),
                Latitude = model?.Latitude?.Trim(),
                UserName = model?.UserName?.Trim(),
            };
            
            // Check if all roles exist
            foreach (var role in model.Roles)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    throw new NotFoundException("One or more roles are invalid.");
                }
            }
            // Create user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ValidatException($"User creation failed: {errorMessages}");
            }
            // Add user to roles
            var addUserRole = await _userManager.AddToRolesAsync(user, model.Roles);
            if (!addUserRole.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ValidatException($"Role creation failed: {errorMessages}");
            }

            return (isSucceed: true, userId: user.Id) ;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }

            if (user.UserName == "Administrator" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");
                //throw new BadRequestException("You can not delete system or admin user");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result =  _mapper.Map<List<UserDTO>>(users);
            return result;
        }

        public async Task<UserDTO> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                Latitude= user.Latitude,
                Longitude= user.Longitude,
                Phone=user.PhoneNumber,
                
            };
            return userDto;
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var result = await _userManager.GetUserIdAsync(user);
            return result;
        }

        public async Task<bool> SigninUserAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            if (!result.Succeeded)
            {
                throw new NotFoundException("Username or password does not match.");
            }
            return true;
        }

        public async Task<bool> UpdateUserProfile(UpdateUserCommand model)
        {
            // Validate input data
            if (string.IsNullOrEmpty(model.id))
            {
                throw new NotFoundException("User ID must be provided.");
            }

            if (model.Roles == null || model.Roles.Count == 0)
            {
                throw new NotFoundException("Role names must be provided.");
            }

            var user = await _userManager.FindByIdAsync(model.id);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            // Check if roles exist
            var roleExists = true;
            foreach (var role in model.Roles)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    roleExists = false;
                    break;
                }
            }

            if (!roleExists)
            {
                throw new NotFoundException("One or more roles are invalid.");
            }
            // Update user properties
            user.FirstName = !string.IsNullOrEmpty(model.FirstName) ? model.FirstName.Trim() : user.FirstName; // Update only if firstName has a value
            user.LastName = !string.IsNullOrEmpty(model.LastName) ? model.LastName.Trim() : user.LastName; // Update only if lastName has a value
            user.Email = !string.IsNullOrEmpty(model.Email) ? model.Email : user.Email;
            user.PhoneNumber = !string.IsNullOrEmpty(model.Phone) ? model.Phone : user.PhoneNumber;
            user.Longitude = model.Longitude;
            user.Latitude = model.Latitude;

        // Perform update operation
        var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new NotFoundException(errorMessages);
            }

            // Update user roles
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesToRemove = userRoles.Except(model.Roles).ToList();
            var rolesToAdd = model.Roles.Except(userRoles).ToList();

            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    var errorMessages = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                    throw new NotFoundException(errorMessages);
                }
            }

            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    var errorMessages = string.Join(", ", addResult.Errors.Select(e => e.Description));
                    throw new NotFoundException(errorMessages);
                }
            }

            return true;
        }
    }
}
