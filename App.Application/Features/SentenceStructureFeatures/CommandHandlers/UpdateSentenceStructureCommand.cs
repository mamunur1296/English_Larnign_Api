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

namespace App.Application.Features.SentenceStructureFeatures.CommandHandlers
{
    public class UpdateSentenceStructureCommand : IRequest<Response<string>>
    {
        public string? id { get; set; }
        public string BanglaSentence { get; set; }
        public string EnglistSentence { get; set; }
        public string? UpdatedBy { get; set; }

    }

    public class UpdateSentenceStructureHandler : IRequestHandler<UpdateSentenceStructureCommand, Response<string>>
    {
        private readonly ISentenceStructureServices _services;

        public UpdateSentenceStructureHandler(ISentenceStructureServices services)
        {
            _services = services;

        }

        public async Task<Response<string>> Handle(UpdateSentenceStructureCommand request, CancellationToken cancellationToken)
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
                    Message = "Sentence Structure  Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the Sentence Structure . Please try again.");
        }
    }
}
