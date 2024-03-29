namespace Application.Appointments.DTOs;

public class AppointmentToCalendarDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string ContactName { get; set; }
    public DateTime Date { get; set; }
    public string[] PhoneNumbers { get; set; }
}