using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SubCategoryFeatures.CommandHandlers
{
    public class CreateAssainFormCommand : IRequest<Response<string>>
    {
        public string SubCategoryId { get; set; }
        public List<string> SentenceFormId { get; set; }
    }
    public class CreateAssainFormHandler : IRequestHandler<CreateAssainFormCommand, Response<string>>
    {

        private readonly ISubCategoryServices _services;


        public CreateAssainFormHandler(ISubCategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<string>> Handle(CreateAssainFormCommand request, CancellationToken cancellationToken)
        {


            var result = await _services.AssainForms(request.SubCategoryId,request.SentenceFormId);

            if (result)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = "Success",
                    Message = "Forms Assaind successfully."
                };
            }
            throw new BadRequestException("Failed to create the Forms Assaind. Please try again.");
        }
    }
}
