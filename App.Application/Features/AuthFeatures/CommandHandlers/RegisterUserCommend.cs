using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace App.Application.Features.AuthFeatures.CommandHandlers
{
    public class RegisterUserCommend : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Phone {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public List<string> Roles { get; set; }
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommend, Response<string>>
    {
        private readonly IUserService _services;
        private readonly IValidator<RegisterUserCommend> _validator;
        private readonly ILogger<RegisterUserHandler> _logger;

        public RegisterUserHandler(IUserService services, IValidator<RegisterUserCommend> validator, ILogger<RegisterUserHandler> logger)
        {
            _services = services;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(RegisterUserCommend request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling user registration for {Email}", request.Email);

            // Validate the request
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("Validation failed for user registration: {Errors}", errorMessages);
                throw new ValidationException($"Validation Error: {errorMessages}");
            }

            // Attempt to create the user
            var result = await _services.CreateUserAsync(request);

            if (result.isSucceed)
            {
                _logger.LogInformation("User {Email} registered successfully", request.Email);
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created,
                    id = result.userId,
                    Message = "Succeed: User registered successfully!"
                };
            }

            _logger.LogError("Failed to register user {Email}", request.Email);
            throw new BadRequestException("Register Error: Failed to register user. Please try again.");
        }
    }

}
