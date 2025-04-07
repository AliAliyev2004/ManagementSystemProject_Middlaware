using ManagementSystem.Application.CQRS.Customers.Commands.Requests;
using ManagementSystem.Application.CQRS.Customers.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) :BadRequest(result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequest request)
    {
        request.Id = id;
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) :BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteCustomerRequest { Id = id });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetByIdCustomerRequest { Id = id });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new  GetAllCustomerRequest());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
