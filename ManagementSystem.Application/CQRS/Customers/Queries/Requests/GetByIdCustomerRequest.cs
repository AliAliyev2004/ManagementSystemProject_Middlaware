using ManagementSystem.Application.CQRS.Customers.Queries.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Queries.Requests;

public class GetByIdCustomerRequest : IRequest<Result<GetByIdCustomerResponse>>
{
    public int Id { get; set; }
}
