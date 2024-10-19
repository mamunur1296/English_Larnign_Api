using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace App.Application.Features.SentenceStructureFeatures.CommandHandlers
{
    public class CreateSentenceStructureFromXlsxFileCommand : IRequest<Response<string>>
    {
        [FromForm]
        public IFormFile  file { get; set; }
    }
    public class CreateSentenceStructureFromXlsxFileHandler : IRequestHandler<CreateSentenceStructureFromXlsxFileCommand, Response<string>>
    {

        private readonly ISentenceStructureServices _services;


        public CreateSentenceStructureFromXlsxFileHandler(ISentenceStructureServices services)
        {
            _services = services;
        }
        public async Task<Response<string>> Handle(CreateSentenceStructureFromXlsxFileCommand request, CancellationToken cancellationToken)
        {

            
            var result = await _services.CreateFormXlsxAsync(request.file);

            if (result)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = "Success",
                    Message = "Xlsx file Uplode  successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Structure . Please try again.");
        }
    }
}
