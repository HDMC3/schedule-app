using Application.Wrappers;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    public Task<Appointment> GetAppointmentById(Guid id);
    public Task<List<Appointment>> GetAppointmentsByDate(DateTime date);
    public Task<DataCollection<Appointment>> GetAppointments(int page, int take);
    public Task CreateAppointment(Appointment appointment);
    public Task DeleteAppointment(Guid id);
    public Task UpdateAppointment(Guid id, Action<Appointment> predicate);
}