using App.Application.DTOs;
using App.Application.Features.AuthFeatures.CommandHandlers;
using App.Application.Features.UserFeatures.CommandHandlers;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        Task<(bool isSucceed, string userId)> CreateUserAsync(RegisterUserCommend model);
        Task<bool> SigninUserAsync(string userName, string password);
        Task<string> GetUserIdAsync(string userName);
        Task<UserDTO> GetUserDetailsAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<bool> UpdateUserProfile(UpdateUserCommand model);
        Task<(bool Success, string ErrorMessage)> ChangePassword(string OldPassword, string newPassword, string Userid);
    }
}
