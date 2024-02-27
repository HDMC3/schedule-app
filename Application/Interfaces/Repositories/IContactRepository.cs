using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IContactRepository
{
    public Task<Contact> GetContactById(Guid id);
    public Task<List<Contact>> GetContacts(int page, int take);
    public Task CreateContact(Contact contact);
    public Task UpdateContact(Contact data);
    public Task DeleteContact(Guid id);
}