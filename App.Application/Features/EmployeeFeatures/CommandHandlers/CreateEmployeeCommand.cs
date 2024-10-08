using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using FluentValidation;
using MediatR;
using System.Net;

namespace App.Application.Features.EmployeeFeatures.CommandHandlers
{
    public class CreateEmployeeCommand : IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Response<string>>
    {
        private readonly IEmployeeServices _services;
        private readonly IValidator<CreateEmployeeCommand> _validator;

        public CreateEmployeeHandler(IEmployeeServices services, IValidator<CreateEmployeeCommand> validator)
        {
            _services = services;
            _validator = validator;
        }

        public async Task<Response<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var result = await _services.CreateAsync(request);

            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Employee created successfully."
                };
            }
            throw new BadRequestException("Failed to create the employee. Please try again.");
        }
    }
}
