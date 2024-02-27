using Application.Interfaces.Repositories;
using Domain.Entities;
using ProductsApp.Data.Extensions;

namespace Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    public readonly DatabaseContext _context;
    public ContactRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Contact> GetContactById(Guid id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null)
        {
            throw new Exception("Contacto no encontrado");
        }

        return contact;
    }

    public async Task<List<Contact>> GetContacts(int page = 1, int take = 10)
    {
        var offset = page - 1 > 0 ? (page - 1) * take : 0;

        var contacts = await _context.Contacts
            .OrderBy(contact => contact.Name)
            .GetPagedAsync(page, take);

        return contacts;
    }

    public async Task CreateContact(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateContact(Contact data)
    {
        var contact = await _context.Contacts.FindAsync(data.Id);
        if (contact == null)
        {
            throw new Exception("Contacto no encontrado");
        }

        contact.Name = data.Name;
        contact.Surname = data.Surname;
        contact.LastModified = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteContact(Guid id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null)
        {
            throw new Exception("Contacto no encontrado");
        }

        _context.Remove(contact);
        await _context.SaveChangesAsync();
    }
}