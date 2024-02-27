using Application.Contacts.DTOs;
using Application.Interfaces.Repositories;
using Application.PhoneNumbers.DTOs;
using MediatR;

namespace Application.Contacts.Queries;

public class GetContactByIdQuery : IRequest<ContactDto>
{
    public Guid Id { get; init; }
}

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto>
{
    IContactRepository _contactRepository;
    public GetContactByIdQueryHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetContactById(request.Id);
        ContactDto contactDto = new()
        {
            Id = contact.Id,
            Name = contact.Name,
            Surname = contact.Surname,
            PhoneNumbers = contact.PhoneNumbers
                .Select(phone => new PhoneNumberDto(phone.Id, phone.Number)).ToList()
        };

        return contactDto;
    }
}
