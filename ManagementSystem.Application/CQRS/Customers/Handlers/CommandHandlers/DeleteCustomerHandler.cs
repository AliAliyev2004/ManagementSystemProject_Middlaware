using ManagementSystem.Application.CQRS.Customers.Commands.Requests;
using ManagementSystem.Application.CQRS.Customers.Commands.Responses;
using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Handlers.CommandHandlers;

public class DeleteCustomerHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerRequest, Result<DeleteCustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DeleteCustomerResponse>> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var success = await _unitOfWork.CustomerRepository.Delete(request.Id, request.DeletedBy);

        if (success == false)
        {
            throw new BadRequestException("Customer not found or already deleted.");
        }

        return new Result<DeleteCustomerResponse>
        {
            Data = new DeleteCustomerResponse { IsDeleted = success },
            IsSuccess = success,
            Errors = []
        };
    }
}
