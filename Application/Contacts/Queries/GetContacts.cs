using Application.Contacts.DTOs;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Contacts.Queries;

public class GetContactsQuery : IRequest<DataCollection<SummaryContactDto>>
{
    public int Page { get; init; }
    public int Take { get; init; }
}

public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, DataCollection<SummaryContactDto>>
{
    IContactRepository _contactRepository;

    public GetContactsQueryHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<DataCollection<SummaryContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _contactRepository.GetContacts(request.Page, request.Take);
        DataCollection<SummaryContactDto> contactsDto = new()
        {
            Page = contacts.Page,
            Pages = contacts.Pages,
            Total = contacts.Total,
            Items = contacts.Items.Select(contact =>
            {
                return new SummaryContactDto(contact.Id, contact.Name, contact.Surname, contact.CreatedAt);
            })
        };

        return contactsDto;
    }
}