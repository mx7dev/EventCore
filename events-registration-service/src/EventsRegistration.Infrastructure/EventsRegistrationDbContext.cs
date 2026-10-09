using EventsRegistration.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventsRegistration.Infrastructure
{
    public class EventsRegistrationDbContext : DbContext, IUnitOfWork
    {
        public EventsRegistrationDbContext(DbContextOptions<EventsRegistrationDbContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<Registration> Registrations => Set<Registration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // El TicketToken es lo que va en el QR: nunca puede repetirse
            modelBuilder.Entity<Registration>()
                .HasIndex(r => r.TicketToken)
                .IsUnique();
        }
    }
}
