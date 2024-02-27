namespace Application.Appointments.DTOs;

public class SummaryAppointmentsDto(Guid id, DateTime date, string contactName)
{
    public Guid Id { get; } = id;
    public DateTime Date { get; } = date;
    public string ContactName { get; } = contactName;
}