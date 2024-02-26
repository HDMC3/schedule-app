using Domain.Common;

namespace Domain.Entities;

public class Contact : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public IList<Appointment> Appointments { get; set; }
    public IList<PhoneNumber> PhoneNumbers { get; set; }
}
