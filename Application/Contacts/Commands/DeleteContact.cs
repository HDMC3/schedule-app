using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Contacts.Commands;

public class DeleteContactCommand : IRequest
{
    public Guid Id { get; init; }
}

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
{
    IContactRepository _contactRepository;

    public DeleteContactCommandHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        await _contactRepository.DeleteContact(request.Id);
    }
}
