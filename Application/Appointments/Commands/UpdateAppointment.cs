using System.ComponentModel.DataAnnotations;
using Application.Contacts.Commands;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Appointments.Commands;

public class UpdateAppointmentCommand : IRequest
{
    [Required]
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
}

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand>
{
    IAppointmentRepository _appointmentRepository;
    public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        await _appointmentRepository.UpdateAppointment(request.Id, appointment =>
        {
            appointment.Date = request.Date ?? appointment.Date;
            appointment.Description = request.Description ?? appointment.Description;
        });
    }
}
