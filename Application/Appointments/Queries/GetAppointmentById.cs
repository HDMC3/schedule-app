using Application.Appointments.DTOs;
using Application.Contacts.DTOs;
using Application.Interfaces.Repositories;
using Application.PhoneNumbers.DTOs;
using MediatR;

namespace Application.Appointments.Queries;

public class GetAppointmentByIdQuery : IRequest<AppointmentDto>
{
    public Guid Id { get; init; }
}

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
    IAppointmentRepository _appointmentRepository;
    public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetAppointmentById(request.Id);

        var appointmentDto = new AppointmentDto
        {
            Id = appointment.Id,
            Date = appointment.Date,
            Description = appointment.Description,
            Contact = new ContactDto
            {
                Id = appointment.Contact.Id,
                Name = appointment.Contact.Name,
                Surname = appointment.Contact.Surname,
                PhoneNumbers = appointment.Contact.PhoneNumbers.Select(phone =>
                {
                    return new PhoneNumberDto(phone.Id, phone.Number);
                }).ToList()
            }
        };

        return appointmentDto;
    }
}
