using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.AuthFeatures.CommandHandlers;
using App.Application.Features.EmployeeFeatures.QueryHandlers;
using App.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;


namespace App.Application.Features.UserFeatures.QueryHandlers
{
    public class GetAllUsersQuery : IRequest<Response<IEnumerable<UserDTO>>>
    {
    }
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserDTO>>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetAllUsersHandler> _logger;

        public GetAllUsersHandler(IUserService userService, ILogger<GetAllUsersHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            // Fetch the list of users
            var users = await _userService.GetAllUsersAsync();

            // Check if no users were found, log, and throw a NotFoundException
            if (users == null || !users.Any())
            {
                _logger.LogWarning("No users found in the system at {Time}.", DateTime.UtcNow);
                throw new NotFoundException("No users found.");
            }

            // Log the successful retrieval of users
            _logger.LogInformation("Successfully retrieved {UserCount} users at {Time}.", users.Count(), DateTime.UtcNow);

            // Return the response with user data
            return new Response<IEnumerable<UserDTO>>
            {
                Success = true,
                Status = HttpStatusCode.OK,
                Data = users,
                Message = "Users retrieved successfully."
            };
        }
    }

}
