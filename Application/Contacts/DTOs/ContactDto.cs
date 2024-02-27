using Application.PhoneNumbers.DTOs;

namespace Application.Contacts.DTOs;

public class ContactDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public List<PhoneNumberDto> PhoneNumbers { get; init; }

}