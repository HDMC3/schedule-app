using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.Property(phoneNumber => phoneNumber.Number)
            .IsRequired()
            .HasMaxLength(8);

        builder.HasOne(phoneNumber => phoneNumber.Contact)
            .WithMany(contact => contact.PhoneNumbers)
            .HasForeignKey(phoneNumber => phoneNumber.ContactId);
    }
}