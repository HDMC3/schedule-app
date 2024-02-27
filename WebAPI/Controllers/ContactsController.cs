using Application.Contacts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    IMediator _mediator;
    public ContactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContactCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}