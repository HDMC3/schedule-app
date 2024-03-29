using Application.Contacts.Commands;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        var contact = await _context.Contacts
            .Include(contact => contact.PhoneNumbers)
            .FirstAsync(contact => contact.Id == id);

        if (contact == null)
        {
            throw new Exception("Contacto no encontrado");
        }

        return contact;
    }

    public async Task<DataCollection<Contact>> GetContacts(int page = 1, int take = 10)
    {
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

    public async Task UpdateContact(UpdateContactCommand data)
    {
        var contact = await _context.Contacts.FindAsync(data.Id);
        if (contact == null)
        {
            throw new Exception("Contacto no encontrado");
        }

        if (data.Name != null)
        {
            contact.Name = data.Name;
        }

        if (data.Surname != null)
        {
            contact.Surname = data.Surname;
        }

        if (data.PhoneNumbers != null)
        {
            await _context.PhoneNumbers
                .Where(phone => phone.ContactId == data.Id)
                .ExecuteDeleteAsync();

            foreach (var phone in data.PhoneNumbers)
            {
                _context.PhoneNumbers.Add(new PhoneNumber
                {
                    Id = phone.Id,
                    Number = phone.Number,
                    ContactId = data.Id,
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow
                });
            }
        }

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