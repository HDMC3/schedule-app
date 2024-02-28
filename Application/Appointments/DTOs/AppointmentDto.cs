namespace Application.Appointments.DTOs;

public class AppointmentDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string ContactName { get; set; }
    public string[] PhoneNumbers { get; set; }
}