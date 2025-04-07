using ManagementSystem.Application.CQRS.Customers.Queries.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Queries.Requests;

public class GetAllCustomerRequest : IRequest<Result<List<GetAllCustomerResponse>>> { }
