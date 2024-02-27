namespace Application.PhoneNumbers.DTOs;

public class PhoneNumberDto(Guid id, string number)
{
    public Guid Id { get; } = id;
    public string Number { get; } = number;
}