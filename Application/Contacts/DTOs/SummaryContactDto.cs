namespace Application.Contacts.DTOs;

public class SummaryContactDto(Guid id, string name, string surname, DateTime createdAt)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string Surname { get; } = surname;
    public DateTime CreateAt { get; } = createdAt;
}