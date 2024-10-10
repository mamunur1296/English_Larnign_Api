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
    public class UpdateVerbCommand : IRequest<Response<string>>
    {
        public string? id { get; set; }
        public string Name { get; set; }
        public string BanglaName { get; set; }
        public string BaseForm { get; set; }
        public string ThirdPersonSingular { get; set; }
        public string PastSimple { get; set; }
        public string PastParticiple { get; set; }
        public string PresentParticiple { get; set; }
        public string Gerund { get; set; }
        public string? UpdatedBy { get; set; }

    }

    public class UpdateVerbHandler : IRequestHandler<UpdateVerbCommand, Response<string>>
    {
        private readonly IVerbServices _services;

        public UpdateVerbHandler(IVerbServices services)
        {
            _services = services;

        }

        public async Task<Response<string>> Handle(UpdateVerbCommand request, CancellationToken cancellationToken)
        {

            var result = await _services.UpdateAsync(request);

            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Verb Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the Verb. Please try again.");
        }
    }
}
