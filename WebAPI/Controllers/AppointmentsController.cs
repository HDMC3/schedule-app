using Application.Appointments.Commands;
using Application.Appointments.DTOs;
using Application.Appointments.Queries;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    IMediator _mediator;
    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DataCollection<SummaryAppointmentsDto>>> GetAll([FromQuery] int page, [FromQuery] int take)
    {
        return await _mediator.Send(new GetAppointmentsQuery { Page = page, Take = take });
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