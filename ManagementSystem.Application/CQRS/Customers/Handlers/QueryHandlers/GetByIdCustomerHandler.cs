using ManagementSystem.Application.CQRS.Customers.Queries.Requests;
using ManagementSystem.Application.CQRS.Customers.Queries.Responses;
using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Handlers.QueryHandlers;

public class GetByIdCustomerHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdCustomerRequest, Result<GetByIdCustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GetByIdCustomerResponse>> Handle(GetByIdCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
        if (customer == null)
        {
            throw new BadRequestException("Customer Not Found");
        }
        return new Result<GetByIdCustomerResponse>
        {
            IsSuccess = true,
            Data = new GetByIdCustomerResponse { Id = customer.Id, Email = customer.Email, Name = customer.Name }
        };
    }
}
