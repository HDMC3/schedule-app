using Domain.Common;

namespace Domain.Entities;

public class PhoneNumber : BaseEntity
{
    public Guid Id { get; set; }
    public string Number { get; set; }

    public Guid ContactId { get; set; }
    public Contact Contact { get; set; }
}