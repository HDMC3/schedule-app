using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Appointments.Commands;

public class CreateExistingContactAppointmentCommand : IRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string? Description { get; set; }

    [Required]
    public Guid ContactId { get; set; }
}

public class CreateExistingContactAppointmentCommandHandler : IRequestHandler<CreateExistingContactAppointmentCommand>
{
    IAppointmentRepository _appointmentRepository;
    public CreateExistingContactAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository
    )
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task Handle(CreateExistingContactAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment
        {
            Id = request.Id,
            Date = request.Date,
            Description = request.Description ?? "",
            CreatedAt = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            ContactId = request.ContactId
        };

        await _appointmentRepository.CreateAppointment(appointment);
    }
}
