using System.ComponentModel.DataAnnotations;
using Application.Contacts.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Appointments.Commands;

public class CreateNoExistingContactAppointmentCommand : IRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string? Description { get; set; }

    [Required]
    public CreateContactCommand Contact { get; set; }
}

public class CreateNoExistingContactAppointmentCommandHandler : IRequestHandler<CreateNoExistingContactAppointmentCommand>
{
    IAppointmentRepository _appointmentRepository;
    public CreateNoExistingContactAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository
    )
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task Handle(CreateNoExistingContactAppointmentCommand request, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            Id = request.Contact.Id,
            Name = request.Contact.Name,
            Surname = request.Contact.Surname ?? "",
            CreatedAt = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            PhoneNumbers = request.Contact.PhoneNumbers
                .Select(phone => new PhoneNumber
                {
                    Id = Guid.NewGuid(),
                    Number = phone,
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow
                }).ToList()
        };

        var appointment = new Appointment
        {
            Id = request.Id,
            Date = request.Date,
            Description = request.Description ?? "",
            CreatedAt = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            Contact = contact
        };

        await _appointmentRepository.CreateAppointment(appointment);
    }
}
