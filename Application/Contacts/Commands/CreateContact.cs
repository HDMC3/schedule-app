using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Contacts.Commands;

public record CreateContactCommand : IRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Surname { get; set; }
    [Required]
    public string[] PhoneNumbers { get; set; }
}

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand>
{
    private readonly IContactRepository _contactRepository;
    public CreateContactCommandHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            Id = request.Id,
            Name = request.Name,
            Surname = request.Surname ?? "",
            CreatedAt = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            PhoneNumbers = request.PhoneNumbers.Select(number => new PhoneNumber
            {
                Id = Guid.NewGuid(),
                Number = number,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            }).ToList()
        };

        await _contactRepository.CreateContact(contact);
    }
}