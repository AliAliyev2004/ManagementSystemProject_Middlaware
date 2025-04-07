using ManagementSystem.Application.CQRS.Customers.Commands.Requests;
using ManagementSystem.Application.CQRS.Customers.Commands.Responses;
using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Handlers.CommandHandlers;

public class UpdateCustomerHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCustomerRequest, Result<UpdateCustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UpdateCustomerResponse>> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
        if (customer == null)
        {
            throw new BadRequestException("Customer Not Found");
        }

        customer.Name = request.Name;
        customer.Email = request.Email;

        _unitOfWork.CustomerRepository.Update(customer);
        return new Result<UpdateCustomerResponse>
        {
            IsSuccess = true,
            Errors = [],
            Data = new UpdateCustomerResponse { Id = request.Id, Name = request.Name, Email = request.Email }
        };
    }
}
