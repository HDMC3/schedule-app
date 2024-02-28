using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProductsApp.Data.Extensions;

namespace Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    public readonly DatabaseContext _context;
    public AppointmentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Appointment> GetAppointmentById(Guid id)
    {
        var appointment = await _context.Appointments
            .Include(appointment => appointment.Contact)
            .FirstAsync(appointment => appointment.Id == id);

        if (appointment == null)
        {
            throw new Exception("Cita no encontrada");
        }

        return appointment;
    }

    public async Task<List<Appointment>> GetAppointmentsByDate(DateTime date)
    {
        var appointments = await _context.Appointments
            .Where(
                appointment => appointment.Date.Day == date.Day
                    && appointment.Date.Month == date.Month
                    && appointment.Date.Year == date.Year
            )
            .Include(appointment => appointment.Contact)
            .ThenInclude(contact => contact.PhoneNumbers)
            .ToListAsync();

        return appointments;
    }

    public async Task<DataCollection<Appointment>> GetAppointments(int page, int take)
    {
        var appointments = await _context.Appointments
            .Where(appointment => appointment.Date >= DateTime.UtcNow)
            .OrderByDescending(appointment => appointment.Date)
            .Include(appointment => appointment.Contact)
            .GetPagedAsync(page, take);

        return appointments;
    }

    public async Task CreateAppointment(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAppointment(Guid id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            throw new Exception("Cita no encontrada");
        }

        _context.Remove(appointment);
        await _context.SaveChangesAsync();
    }

}
