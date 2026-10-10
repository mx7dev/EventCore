using EventsRegistration.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsRegistration.Infrastructure.Persistence.Configurations
{
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(r => r.Id);

            // El TicketToken es lo que va en el QR: nunca puede repetirse
            builder.HasIndex(r => r.TicketToken)
                   .IsUnique();
        }
    }
}
