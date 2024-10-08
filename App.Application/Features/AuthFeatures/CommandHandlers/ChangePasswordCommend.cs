using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.AuthFeatures.CommandHandlers
{
    public class ChangePasswordCommend : IRequest<Response<string>>
    {
       
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserId { get; set; }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommend, Response<string>>
    {
        private readonly IUserService _services;


        public ChangePasswordHandler(IUserService services)
        {
            _services = services;
          
        }

        public async Task<Response<string>> Handle(ChangePasswordCommend request, CancellationToken cancellationToken)
        {

            var result = await _services.ChangePassword(request.OldPassword,request.NewPassword,request.UserId);
            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = "Success",
                    Message = "Password change  successfully."
                };
            }
            throw new BadRequestException("Failed to Change the User password . Please try again.");
        }
    }
}
