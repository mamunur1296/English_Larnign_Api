using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Features.CategoryFeatures.CommandHandlers;
using App.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.VerbFeatures.CommandHandlers
{
    public class DeleteVerbCommand : IRequest<Response<string>>
    {
        public DeleteVerbCommand(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }
    public class DeleteVerbHandler : IRequestHandler<DeleteVerbCommand, Response<string>>
    {

        private readonly IVerbServices _services;


        public DeleteVerbHandler(IVerbServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteVerbCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.DeleteAsync(request.id);
            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.OK, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Verb Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the Verb. Please try again.");
        }
    }
}
