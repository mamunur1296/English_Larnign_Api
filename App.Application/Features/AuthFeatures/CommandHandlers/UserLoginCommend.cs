using App.Application.Common;
using App.Application.DTOs;
using App.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace App.Application.Features.AuthFeatures.CommandHandlers
{
    public class UserLoginCommend : IRequest<Response<AuthDTO>>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserLoginHandelar : IRequestHandler<UserLoginCommend, Response<AuthDTO>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserLoginHandelar> _logger;
        private readonly ITokenGenerator _tokenGenerator;

        public UserLoginHandelar(IUserService userService, ILogger<UserLoginHandelar> logger, ITokenGenerator tokenGenerator)
        {
            _userService = userService;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Response<AuthDTO>> Handle(UserLoginCommend request, CancellationToken cancellationToken)
        {
            var result = await _userService.SigninUserAsync(request.UserName, request.Password);
            if (!result)
            {
                throw new Exception("Invalid username or password");
            }
            var user = await _userService.GetUserDetailsAsync(await _userService.GetUserIdAsync(request.UserName));

            string token = _tokenGenerator.GenerateJWTToken((user.Id, user.UserName, user.FirstName, user.LastName, user.Email, user.Roles));
            var auth = new AuthDTO()
            {
                UserId = user.Id,
                Name = user.UserName,
                Token = token,
            };

            // Return the response with user data
            return new Response<AuthDTO>
            {
                Success = true,
                Data = auth,
                Status = HttpStatusCode.OK,
                Message = "User Lgoin successfully."
            };
        }
    }
}
