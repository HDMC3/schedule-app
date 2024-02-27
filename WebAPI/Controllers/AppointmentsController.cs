using Application.Appointments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentController : ControllerBase
{
    IMediator _mediator;
    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("contact")]
    public async Task<IActionResult> Create(CreateExistingContactAppointmentCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpPost]
    [Route("no-contact")]
    public async Task<IActionResult> Create(CreateNoExistingContactAppointmentCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}