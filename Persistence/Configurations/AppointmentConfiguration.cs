using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(appointment => appointment.Date)
            .IsRequired();

        builder.HasOne(appointment => appointment.Contact)
            .WithMany(contact => contact.Appointments)
            .HasForeignKey(appointment => appointment.ContactId);
    }
}