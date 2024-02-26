using Domain.Common;

namespace Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }

    public Guid ContactId { get; set; }
    public Contact Contact { get; set; }
}