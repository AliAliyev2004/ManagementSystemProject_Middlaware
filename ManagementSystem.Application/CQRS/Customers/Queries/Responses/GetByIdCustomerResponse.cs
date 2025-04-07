namespace ManagementSystem.Application.CQRS.Customers.Queries.Responses;

public class GetByIdCustomerResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
