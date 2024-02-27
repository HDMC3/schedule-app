using Application.Contacts.Commands;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IContactRepository
{
    public Task<Contact> GetContactById(Guid id);
    public Task<DataCollection<Contact>> GetContacts(int page, int take);
    public Task CreateContact(Contact contact);
    public Task UpdateContact(UpdateContactCommand data);
    public Task DeleteContact(Guid id);
}