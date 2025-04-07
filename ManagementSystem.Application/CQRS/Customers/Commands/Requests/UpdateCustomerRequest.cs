using ManagementSystem.Application.CQRS.Customers.Commands.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Commands.Requests;

public class UpdateCustomerRequest : IRequest<Result<UpdateCustomerResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
