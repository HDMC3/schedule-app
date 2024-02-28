using Application.Contacts.DTOs;

namespace Application.Appointments.DTOs;

public class AppointmentDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public ContactDto Contact { get; set; }
}