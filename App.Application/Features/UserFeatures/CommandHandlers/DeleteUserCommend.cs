using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.UserFeatures.CommandHandlers
{
    public class DeleteUserCommend : IRequest<Response<string>>
    {
        public DeleteUserCommend(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommend, Response<string>>
    {
        private readonly IUserService _userService;

        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Response<string>> Handle(DeleteUserCommend request, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteUserAsync(request.Id);
            if (result)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.OK, // You can set the status code here
                    id = request.Id,
                    Data = "Success",
                    Message = "User Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the User. Please try again.");
        }
    }
}
