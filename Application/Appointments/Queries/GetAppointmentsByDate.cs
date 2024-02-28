using Application.Appointments.DTOs;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Appointments.Queries;

public class GetAppointmentsByDateQuery : IRequest<List<AppointmentDto>>
{
    public DateTime Date { get; init; }
}

public class GetAppointmentsByDateQueryHandler : IRequestHandler<GetAppointmentsByDateQuery, List<AppointmentDto>>
{
    IAppointmentRepository _appointmentRepository;
    public GetAppointmentsByDateQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<List<AppointmentDto>> Handle(GetAppointmentsByDateQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAppointmentsByDate(request.Date);

        var appointmentsDto = appointments.Select(appointment =>
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                Description = appointment.Description,
                ContactName = appointment.Contact.Name + " " + appointment.Contact.Surname,
                PhoneNumbers = appointment.Contact.PhoneNumbers.Select(phone => phone.Number).ToArray()
            };
        }).ToList();

        return appointmentsDto;
    }
}
