using Application.Contacts.Commands;
using Application.Contacts.DTOs;
using Application.Contacts.Queries;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ContactDto>> Get(Guid id)
    {
        var contact = await _mediator.Send(new GetContactByIdQuery { Id = id });
        return Ok(contact);
    }

    [HttpGet]
    public async Task<ActionResult<DataCollection<SummaryContactDto>>> GetAll([FromQuery] int page, [FromQuery] int take)
    {
        GetContactsQuery query = new()
        {
            Page = page,
            Take = take
        };
        var contacts = await _mediator.Send(query);
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContactCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}