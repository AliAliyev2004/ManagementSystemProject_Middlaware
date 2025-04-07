using ManagementSystem.Application.CQRS.Customers.Commands.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Commands.Requests;

public class DeleteCustomerRequest : IRequest<Result<DeleteCustomerResponse>>
{
    public int Id { get; set; }
    public int DeletedBy { get; set; }
}
