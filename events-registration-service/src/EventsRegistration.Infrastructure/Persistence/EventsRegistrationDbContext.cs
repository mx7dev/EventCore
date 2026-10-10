using EventsRegistration.Domain.Common;
using EventsRegistration.Domain.Events;
using EventsRegistration.Domain.Registrations;
using Microsoft.EntityFrameworkCore;

namespace EventsRegistration.Infrastructure.Persistence
{
    public class EventsRegistrationDbContext : DbContext, IUnitOfWork
    {
        public EventsRegistrationDbContext(DbContextOptions<EventsRegistrationDbContext> options) : base(options) { }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<Registration> Registrations => Set<Registration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todas las clases IEntityTypeConfiguration de este proyecto
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsRegistrationDbContext).Assembly);
        }

    }
}
