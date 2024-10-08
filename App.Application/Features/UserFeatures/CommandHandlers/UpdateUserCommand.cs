using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.UserFeatures.CommandHandlers
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public UpdateUserCommand(string? id)
        {
            this.id = id;
        }

        public string? id { get; set; }
        public string? FirstName { get; set; }
        public string ?LastName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string ?Email { get; set; }
        public string ?Phone { get; set; }
        public List<string> ? Roles { get; set; }
    }
    public  class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response<string>>
    {
        private readonly IUserService _services;

        public UpdateUserHandler(IUserService services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.UpdateUserProfile(request);
            if (result)
            {
              
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created,
                    id = request.id,
                    Message = "Succeed: User registered successfully!"
                };
            }

           
            throw new BadRequestException("Register Error: Failed to register user. Please try again.");
        }
    }
}
