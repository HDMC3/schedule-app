using Application.Appointments.DTOs;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Appointments.Queries;

public class GetAppointmentsQuery : IRequest<DataCollection<SummaryAppointmentsDto>>
{
    public int Page { get; init; }
    public int Take { get; init; }
}

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, DataCollection<SummaryAppointmentsDto>>
{
    IAppointmentRepository _appointmentRepository;

    public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<DataCollection<SummaryAppointmentsDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAppointments(request.Page, request.Take);

        var appointmentsDto = new DataCollection<SummaryAppointmentsDto>
        {
            Page = appointments.Page,
            Pages = appointments.Pages,
            Total = appointments.Total,
            Items = appointments.Items.Select(appointment =>
            {
                return new SummaryAppointmentsDto(
                    appointment.Id,
                    appointment.Date,
                    appointment.Contact.Name + " " + appointment.Contact.Surname
                );
            })
        };

        return appointmentsDto;
    }
}
