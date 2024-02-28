using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Appointments.Commands;

public class DeleteAppointmentCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
{
    IAppointmentRepository _appointmentRepository;
    public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        await _appointmentRepository.DeleteAppointment(request.Id);
    }
}
