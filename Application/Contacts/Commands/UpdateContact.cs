using Application.Interfaces.Repositories;
using Application.PhoneNumbers.DTOs;
using MediatR;

namespace Application.Contacts.Commands;

public class UpdateContactCommand : IRequest
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    // public List<Guid> PhoneNumbersToDelete { get; set; }
    // public List<PhoneNumberDto> PhoneNumbersToEdit { get; set; }
    // public List<PhoneNumberDto> PhoneNumbersToAdd { get; set; }
    public List<PhoneNumberDto>? PhoneNumbers { get; set; }
}

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
{
    IContactRepository _contactRepository;

    public UpdateContactCommandHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        await _contactRepository.UpdateContact(request);
    }
}