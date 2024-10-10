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
    public class DeleteSentenceStructureCommand : IRequest<Response<string>>
    {
        public DeleteSentenceStructureCommand(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }
    public class DeleteSentenceStructureHandler : IRequestHandler<DeleteSentenceStructureCommand, Response<string>>
    {

        private readonly ISentenceStructureServices _services;


        public DeleteSentenceStructureHandler(ISentenceStructureServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteSentenceStructureCommand request, CancellationToken cancellationToken)
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
                    Message = "Sentence Structure  Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the Sentence Structure . Please try again.");
        }
    }
}
