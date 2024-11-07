using App.Application.Common;
using App.Application.Exceptions;
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
    public class DeleteAllSentenceStructureCommand : IRequest<Response<string>>
    {
        
    }
    public class DeleteAllSentenceStructureHandler : IRequestHandler<DeleteAllSentenceStructureCommand, Response<string>>
    {

        private readonly ISentenceStructureServices _services;


        public DeleteAllSentenceStructureHandler(ISentenceStructureServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteAllSentenceStructureCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.DeleteAll();
            if (result)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.OK, // You can set the status code here
                    Data = "Success",
                    Message = "Sentence Structure  All Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the Sentence Structure . Please try again.");
        }
    }
}
